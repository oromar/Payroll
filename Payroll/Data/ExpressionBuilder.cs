using System;
using System.Collections.Generic;
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
    LIKE
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
    private ParameterExpression entity;
    private List<Statement> expressions;
    public ExpressionBuilder()
    {
        entity = Expression.Parameter(typeof(T), Constants.ENTITY);
        expressions = new List<Statement>();
    }
    public ExpressionBuilder<T> Where(string filterName, LogicOperator operation, object filterValue)
    {
        CreateExpression(filterName, filterValue, operation, SqlConnector.AND);
        return this;
    }
    public ExpressionBuilder<T> And(string filterName, LogicOperator operation, object filterValue)
    {
        CreateExpression(filterName, filterValue, operation, SqlConnector.AND);
        return this;
    }
    public ExpressionBuilder<T> Or(string filterName, LogicOperator operation, object filterValue)
    {
        CreateExpression(filterName, filterValue, operation, SqlConnector.OR);
        return this;
    }
    public Expression<Func<T, bool>> Build()
    {
        var result = expressions[0].Expression;

        if (expressions.Count > 1)
        {
            for (int i = 1; i < expressions.Count; i++)
            {
                if (expressions[i].SqlConnector == SqlConnector.AND)
                {
                    result = Expression.Lambda<Func<T, bool>>(
                    Expression.And(Expression.Invoke(result, entity), Expression.Invoke(expressions[i].Expression, entity)),
                    entity);
                }
                else
                {
                    result = Expression.Lambda<Func<T, bool>>(
                    Expression.Or(Expression.Invoke(result, entity), Expression.Invoke(expressions[i].Expression, entity)),
                    entity);
                }
            }
        }
        return result;
    }
    private Expression GetPropertyExpression(string filterName)
    {
        const string PATH_SEPARATOR = ".";
        Expression result;
        if (filterName.Contains(PATH_SEPARATOR))
        {
            Expression relatedEntity = null;
            var tokens = filterName.Split(PATH_SEPARATOR);
            for (int i = 0; i < tokens.Length - 1; i++)
            {
                relatedEntity = Expression.Property(relatedEntity ?? entity, tokens[i]);
            }
            result = Expression.Property(relatedEntity, tokens[tokens.Length - 1]);
        }
        else
        {
            result = Expression.Property(entity, filterName);
        }
        return result;
    }
    private Expression GetStatement(object filterValue, Expression property, Expression constant, LogicOperator operation)
    {
        Expression statement = null;
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
            case LogicOperator.LIKE:
                if (filterValue is String)
                {
                    var normalized = filterValue.ToString().RemoveDiacritics().Trim();
                    constant = Expression.Constant(normalized);
                    statement = Expression.Call(property, typeof(string).GetMethod(Constants.CONTAINS, new[] { typeof(string) }), constant);
                }
                else
                {
                    statement = Expression.Equal(property, constant);
                }
                break;
            default:
                //does nothing
                break;
        }
        return statement;
    }
    private void CreateExpression(string filterName, object filterValue, LogicOperator operation, SqlConnector connector)
    {
        var property = GetPropertyExpression(filterName);
        var constant = Expression.Constant(filterValue);
        var statement = GetStatement(filterValue, property, constant, operation);
        if (statement != null)
        {
            expressions.Add(new Statement
            {
                Expression = Expression.Lambda<Func<T, bool>>(statement, entity),
                SqlConnector = connector
            });
        }
    }
}

