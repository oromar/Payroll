using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MMLib.Extensions;
using Payroll.Common;

public enum LogicOperator
{
    LESS_THAN = 1,
    LESS_OR_EQUAL_THAN,
    GREATHER_THAN,
    GREATHER_OR_EQUAL_THAN,
    EQUALS,
    LIKE,
    IN
}
public enum SqlConnector
{
    AND = 1,
    OR
}

public class ExpressionBuilder<T> where T : class
{
    private class Statement
    {
        public SqlConnector SqlConnector { get; set; }
        public Expression<Func<T, bool>> Expression { get; set; }
    }
    private const string ENTITY = "entity";
    private const string PATH_SEPARATOR = ".";
    private const string CONTAINS = "Contains";
    private ParameterExpression entity;
    private List<Statement> statements;
    private bool multTerms = false;
    private SqlConnector multTermsConnector = SqlConnector.OR;
    public ExpressionBuilder()
    {
        entity = Expression.Parameter(typeof(T), ENTITY);
        statements = new List<Statement>();
    }
    public ExpressionBuilder<T> EnableMultTerms(SqlConnector connector)
    {
        multTerms = true;
        multTermsConnector = connector;
        return this;
    }
    public ExpressionBuilder<T> Where(string filterName, LogicOperator operation, object filterValue)
    {
        AddStatement(filterName, filterValue, operation, SqlConnector.AND);
        return this;
    }
    public ExpressionBuilder<T> And(string filterName, LogicOperator operation, object filterValue)
    {
        AddStatement(filterName, filterValue, operation, SqlConnector.AND);
        return this;
    }
    public ExpressionBuilder<T> Or(string filterName, LogicOperator operation, object filterValue)
    {
        AddStatement(filterName, filterValue, operation, SqlConnector.OR);
        return this;
    }
    public Expression<Func<T, bool>> Build()
    {
        var result = statements[0].Expression;

        if (statements.Count > 1)
        {
            for (int i = 1; i < statements.Count; i++)
            {
                if (statements[i].SqlConnector == SqlConnector.AND)
                {
                    result = Expression.Lambda<Func<T, bool>>(
                    Expression.And(Expression.Invoke(result, entity), Expression.Invoke(statements[i].Expression, entity)),
                    entity);
                }
                else
                {
                    result = Expression.Lambda<Func<T, bool>>(
                    Expression.Or(Expression.Invoke(result, entity), Expression.Invoke(statements[i].Expression, entity)),
                    entity);
                }
            }
        }
        return result;
    }
    private Expression GetPropertyExpression(string filterName)
    {
        Expression property;
        if (filterName.Contains(PATH_SEPARATOR))
        {
            Expression relatedEntity = null;
            var tokens = filterName.Split(PATH_SEPARATOR);
            for (int i = 0; i < tokens.Length - 1; i++)
            {
                relatedEntity = Expression.Property(relatedEntity ?? entity, tokens[i]);
            }
            property = Expression.Property(relatedEntity, tokens[tokens.Length - 1]);
        }
        else
        {
            property = Expression.Property(entity, filterName);
        }
        return property;
    }
    private Expression GetStatement(object filterValue, Expression property, LogicOperator operation)
    {
        Expression statement = null;
        var constant = Expression.Constant(filterValue);
        switch (operation)
        {
            case LogicOperator.EQUALS:
                statement = Expression.Equal(property, constant);
                break;
            case LogicOperator.GREATHER_THAN:
                statement = Expression.GreaterThan(property, constant);
                break;
            case LogicOperator.LESS_THAN:
                statement = Expression.LessThan(property, constant);
                break;
            case LogicOperator.GREATHER_OR_EQUAL_THAN:
                statement = Expression.GreaterThanOrEqual(property, constant);
                break;
            case LogicOperator.LESS_OR_EQUAL_THAN:
                statement = Expression.LessThanOrEqual(property, constant);
                break;
            case LogicOperator.IN:
                if (!(filterValue is object[])) return null;
                statement = GetInStatement(filterValue as object[], property);
                break;
            case LogicOperator.LIKE:
                statement = GetLikeOrEqualStatement(filterValue, property);
                break;
            default:
                //does nothing
                break;
        }
        return statement;
    }
    private Expression GetInStatement(object[] items, Expression property)
    {
        if (items == null || !items.Any()) return null;
        Expression statement = GetLikeOrEqualStatement(items[0], property);
        if (items.Count() > 1)
        {
            for (int i = 1; i < items.Count(); i++)
            {
                statement = Expression.Or(statement, GetLikeOrEqualStatement(items[i], property));
            }
        }
        return statement;
    }
    private Expression GetLikeOrEqualStatement(object filterValue, Expression property)
    {
        Expression statement = null;
        if (filterValue is String)
        {
            var normalized = filterValue.ToString().RemoveDiacritics().Trim();
            if (multTerms)
            {
                statement = GetMultTermsStatement(normalized, property);
            }
            else
            {
                statement = Expression.Call(property,
                    typeof(string).GetMethod(CONTAINS, new[] { typeof(string) }), Expression.Constant(normalized));
            }
        }
        else
        {
            statement = Expression.Equal(property, Expression.Constant(filterValue));
        }
        return statement;
    }
    private void AddStatement(string filterName, object filterValue, LogicOperator operation, SqlConnector connector)
    {
        var property = GetPropertyExpression(filterName);
        var statement = GetStatement(filterValue, property, operation);
        if (statement != null)
        {
            statements.Add(new Statement
            {
                Expression = Expression.Lambda<Func<T, bool>>(statement, entity),
                SqlConnector = connector
            });
        }
    }
    private Expression GetMultTermsStatement(string value, Expression property)
    {
        Expression constant;
        Expression statement = null;
        List<Expression> expressions = new List<Expression>();
        var tokens = GetTokens(value);
        foreach (var token in tokens)
        {
            constant = Expression.Constant(token);
            statement = Expression.Call(property, typeof(string).GetMethod(CONTAINS, new[] { typeof(string) }), constant);
            expressions.Add(statement);
        }
        statement = expressions[0];
        for (int i = 1; i < expressions.Count; i++)
        {
            statement = multTermsConnector == SqlConnector.OR ?
                    Expression.Or(statement, expressions[i]) :
                    Expression.And(statement, expressions[i]);
        }
        return statement;
    }
    private string[] GetTokens(string value)
    {
        const string WHITE_SPACE = " ";
        const string SLASH = "/";
        const string AMPERSAND = "&";
        const string SEMI_COLON = ";";
        const string COLON = ",";
        if (value.Contains(WHITE_SPACE)) return value.Split(WHITE_SPACE);
        if (value.Contains(SLASH)) return value.Split(SLASH);
        if (value.Contains(AMPERSAND)) return value.Split(AMPERSAND);
        if (value.Contains(SEMI_COLON)) return value.Split(SEMI_COLON);
        if (value.Contains(COLON)) return value.Split(COLON);
        return new string[] { value };
    }
}

