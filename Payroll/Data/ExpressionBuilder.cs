using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MMLib.Extensions;
using Payroll.Common;

namespace ExpressionUtils
{
    public enum Operator
    {
        LESS_THAN = 1,
        LESS_OR_EQUAL_THAN,
        GREATHER_THAN,
        GREATHER_OR_EQUAL_THAN,
        EQUALS,
        LIKE,
        ILIKE,
        STARTS_WITH,
        ENDS_WITH,
        IN,
        NOT_IN
    }
    public enum SqlConnector
    {
        AND = 1,
        OR,
        XOR
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
        private const string STARTS_WITH = "StartsWith";
        private const string ENDS_WITH = "EndsWith";
        private const string TO_LOWER = "ToLower";
        private readonly ParameterExpression entity;
        private readonly List<Statement> statements;
        private bool multTerms = false;
        private SqlConnector multTermsConnector = SqlConnector.OR;
        private string multTermsSeparator = string.Empty;
        private bool incrementalSearch = false;
        private int incrementalSearchStep = 1;
        public ExpressionBuilder()
        {
            entity = Expression.Parameter(typeof(T), ENTITY);
            statements = new List<Statement>();
        }
        public ExpressionBuilder<T> EnableIncrementalSearch(int step = 1)
        {
            if (multTerms) throw new ArgumentException("Cannot enable incremental search and mult terms search simultaneously, please choose only one option");
            incrementalSearch = true;
            incrementalSearchStep = step;
            return this;
        }
        public ExpressionBuilder<T> DisableIncrementalSearch()
        {
            incrementalSearch = false;
            incrementalSearchStep = 1;
            return this;
        }
        public ExpressionBuilder<T> EnableMultTerms(SqlConnector connector, string separator = " ")
        {
            if (incrementalSearch) throw new ArgumentException("Cannot enable mult terms search and incremental search simultaneously, please choose only one option");
            multTerms = true;
            multTermsConnector = connector;
            multTermsSeparator = separator;
            return this;
        }
        public ExpressionBuilder<T> DisableMultTerms()
        {
            multTerms = false;
            return this;
        }
        public ExpressionBuilder<T> Where(string filterName, Operator operation, object filterValue)
        {
            AddStatement(filterName, filterValue, operation, SqlConnector.AND);
            return this;
        }
        public ExpressionBuilder<T> Where(Expression<Func<T, bool>> predicate)
        {
            AddStatement(predicate, SqlConnector.AND);
            return this;
        }
        public ExpressionBuilder<T> And(string filterName, Operator operation, object filterValue) => Where(filterName, operation, filterValue);
        public ExpressionBuilder<T> And(Expression<Func<T, bool>> predicate) => Where(predicate);
        public ExpressionBuilder<T> Or(string filterName, Operator operation, object filterValue)
        {
            AddStatement(filterName, filterValue, operation, SqlConnector.OR);
            return this;
        }
        public ExpressionBuilder<T> Or(Expression<Func<T, bool>> predicate)
        {
            AddStatement(predicate, SqlConnector.OR);
            return this;
        }
        public ExpressionBuilder<T> Xor(string filterName, Operator operation, object filterValue)
        {
            AddStatement(filterName, filterValue, operation, SqlConnector.XOR);
            return this;
        }
        public ExpressionBuilder<T> Xor(Expression<Func<T, bool>> predicate)
        {
            AddStatement(predicate, SqlConnector.XOR);
            return this;
        }

        public Expression<Func<T, bool>> Build()
        {
            if (!statements.Any()) return null;

            var result = statements[0].Expression;

            if (statements.Count > 1)
            {
                for (int i = 1; i < statements.Count; i++)
                {
                    switch (statements[i].SqlConnector)
                    {
                        case SqlConnector.AND:
                            result = Expression.Lambda<Func<T, bool>>(
                            Expression.And(
                                Expression.Invoke(result, entity),
                                Expression.Invoke(statements[i].Expression, entity)),
                            entity);
                            break;
                        case SqlConnector.OR:
                            result = Expression.Lambda<Func<T, bool>>(
                            Expression.Or(
                                Expression.Invoke(result, entity),
                                Expression.Invoke(statements[i].Expression, entity)),
                            entity);
                            break;
                        case SqlConnector.XOR:

                            result = Expression.Lambda<Func<T, bool>>(
                            Expression.Or(
                                Expression.And(
                                    Expression.Not(Expression.Invoke(result, entity)),
                                    Expression.Invoke(statements[i].Expression, entity)),
                                Expression.And(
                                    Expression.Invoke(result, entity),
                                    Expression.Not(Expression.Invoke(statements[i].Expression, entity)))
                            ),
                            entity);
                            break;
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
        private Expression GetStatement(object filterValue, Expression property, Operator operation)
        {
            Expression statement = null;
            var constant = Expression.Constant(filterValue);
            switch (operation)
            {
                case Operator.EQUALS:
                    statement = Expression.Equal(property, constant);
                    break;
                case Operator.GREATHER_THAN:
                    statement = Expression.GreaterThan(property, constant);
                    break;
                case Operator.LESS_THAN:
                    statement = Expression.LessThan(property, constant);
                    break;
                case Operator.GREATHER_OR_EQUAL_THAN:
                    statement = Expression.GreaterThanOrEqual(property, constant);
                    break;
                case Operator.LESS_OR_EQUAL_THAN:
                    statement = Expression.LessThanOrEqual(property, constant);
                    break;
                case Operator.IN:
                    if (!(filterValue is object[])) return null;
                    statement = GetInStatement(filterValue as object[], property);
                    break;
                case Operator.NOT_IN:
                    if (!(filterValue is object[])) return null;
                    statement = Expression.Not(GetInStatement(filterValue as object[], property));
                    break;
                case Operator.LIKE:
                    statement = GetLikeStatement(filterValue, property);
                    break;
                case Operator.ILIKE:
                    statement = GetILikeStatement(filterValue, property);
                    break;
                case Operator.STARTS_WITH:
                    statement = GetStartsWithStatement(filterValue, property);
                    break;
                case Operator.ENDS_WITH:
                    statement = GetEndsWithStatement(filterValue, property);
                    break;
                default:
                    //does nothing
                    break;
            }
            return statement;
        }
        private Expression GetStartsWithStatement(object filterValue, Expression property)
        {
            if (filterValue == null) return null;
            return Expression.Call(property,
                       typeof(string).GetMethod(STARTS_WITH, new[] { typeof(string) }),
                                Expression.Constant(filterValue.ToString().RemoveDiacritics().Trim()));
        }
        private Expression GetEndsWithStatement(object filterValue, Expression property)
        {
            if (filterValue == null) return null;
            return Expression.Call(property,
                        typeof(string).GetMethod(ENDS_WITH, new[] { typeof(string) }),
                                Expression.Constant(filterValue.ToString().RemoveDiacritics().Trim()));
        }
        private Expression GetInStatement(object[] items, Expression property)
        {
            if (items == null || !items.Any()) return null;
            Expression statement = GetLikeStatement(items[0], property);
            if (items.Count() > 1)
            {
                for (int i = 1; i < items.Count(); i++)
                {
                    statement = Expression.Or(statement, GetLikeStatement(items[i], property));
                }
            }
            return statement;
        }
        private Expression GetLikeStatement(object filterValue, Expression property)
        {
            Expression statement;
            if (filterValue == null) return null;
            var normalized = filterValue.ToString().RemoveDiacritics().Trim();
            if (multTerms)
            {
                statement = GetMultTermsStatement(normalized, property);
            }
            else if (incrementalSearch)
            {
                statement = GetIncrementalSearchStatement(normalized, property);
            }
            else
            {
                statement = Expression.Call(property,
                    typeof(string).GetMethod(
                        CONTAINS, new[] { typeof(string) }), Expression.Constant(normalized)
                        );
            }
            return statement;
        }
        private Expression GetILikeStatement(object filterValue, Expression property)
        {
            Expression statement;
            if (filterValue == null) return null;
            var normalized = filterValue.ToString().ToLower().RemoveDiacritics().Trim();
            var toLowerProperty = Expression.Call(property, typeof(string).GetMethod(TO_LOWER, new Type[] { }));
            if (multTerms)
            {
                statement = GetMultTermsStatement(normalized, toLowerProperty);
            }
            else if (incrementalSearch)
            {
                statement = GetIncrementalSearchStatement(normalized, toLowerProperty);
            }
            else
            {
                statement = Expression.Call(toLowerProperty,
                    typeof(string).GetMethod(
                        CONTAINS, new[] { typeof(string) }), Expression.Constant(normalized)
                        );
            }
            return statement;
        }
        private void AddStatement(Expression<Func<T, bool>> predicate, SqlConnector connector)
        {
            statements.Add(new Statement
            {
                SqlConnector = connector,
                Expression = predicate,
            });
        }

        private void AddStatement(string filterName, object filterValue, Operator operation, SqlConnector connector)
        {
            if (string.IsNullOrWhiteSpace(filterName)) throw new ArgumentException("Filter name is required for non Lambda filters");
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
        private Expression GetIncrementalSearchStatement(string value, Expression property)
        {
            Expression result = null;
            var current = incrementalSearchStep;
            var totalLetters = value.Count();
            while (current <= totalLetters)
            {
                var currentToken = new string(value.Take(current).ToArray());
                var constant = Expression.Constant(currentToken);
                var statement = Expression.Call(property, typeof(string).GetMethod(Constants.CONTAINS, new[] { typeof(string) }), constant);
                result = result == null ? statement : Expression.Or(result, statement) as Expression;
                if (current >= totalLetters) break;
                current = (current + incrementalSearchStep) < totalLetters ? (current + incrementalSearchStep) : totalLetters;
            }
            return result;
        }
        private Expression GetMultTermsStatement(string value, Expression property)
        {
            Expression constant;
            Expression statement;
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
            return value.Split(multTermsSeparator);
        }
    }
}
