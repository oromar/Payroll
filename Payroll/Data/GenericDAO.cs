using ExpressionUtils;
using Microsoft.EntityFrameworkCore;
using MMLib.Extensions;
using Payroll.Common;
using Payroll.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
namespace Payroll.Data
{
    public class GenericDAO<T> where T : Basic
    {
        private readonly ApplicationDbContext _context;

        public GenericDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        // public Expression<Func<T, bool>> FilterBy(string filter)
        // {
        //     var expressions = new List<Expression<Func<T, bool>>>();

        //     var parameter = Expression.Parameter(typeof(T), Constants.ENTITY);

        //     var isDeletedProperty = Expression.Property(parameter, Constants.IS_DELETED);
        //     var falseConstant = Expression.Constant(false);
        //     var notDeleted = Expression.Equal(isDeletedProperty, falseConstant);
        //     var notDeletedExpression = Expression.Lambda<Func<T, bool>>(notDeleted, parameter);
        //     if (filter.IsNullOrEmpty() || filter.Equals(Constants.QUERY_SEPARATOR)) return notDeletedExpression;

        //     if (Constants.INCREMENTAL_SEARCH_ACTIVE)
        //     {
        //         var step = 3;
        //         var current = step;
        //         var normalizedFilter = filter.RemoveDiacritics().Trim();
        //         var totalLetters = normalizedFilter.Count();
        //         while (current <= totalLetters)
        //         {
        //             var currentToken = new string(normalizedFilter.Take(current).ToArray());
        //             var constant = Expression.Constant(currentToken);
        //             var property = Expression.Property(parameter, Constants.SEARCH_FIELDS);
        //             var contains = Expression.Call(property, typeof(string).GetMethod(Constants.CONTAINS, new[] { typeof(string) }), constant);
        //             var expression = Expression.Lambda<Func<T, bool>>(contains, parameter);
        //             expressions.Add(expression);
        //             if (current >= totalLetters) break;
        //             current = (current + step) < totalLetters ? (current + step) : totalLetters;
        //         }
        //     }
        //     else
        //     {
        //         var tokens = filter.Split(Constants.QUERY_SEPARATOR);
        //         foreach (var token in tokens.Where(a => !string.IsNullOrWhiteSpace(a)))
        //         {
        //             var normalizedFilter = token.RemoveDiacritics().Trim();
        //             var constantParameter = Expression.Constant(normalizedFilter);
        //             var propertyName = Expression.Property(parameter, Constants.SEARCH_FIELDS);
        //             var containsMethod = Expression.Call(propertyName, typeof(string).GetMethod(Constants.CONTAINS, new[] { typeof(string) }), constantParameter);
        //             var containsExpression = Expression.Lambda<Func<T, bool>>(containsMethod, parameter);
        //             expressions.Add(containsExpression);
        //         }

        //     }

        //     var result = expressions[0];

        //     if (expressions.Count > 1)
        //     {
        //         for (int i = 1; i < expressions.Count; i++)
        //         {
        //             result = Constants.INCREMENTAL_SEARCH_ACTIVE ? Expression.Lambda<Func<T, bool>>(
        //                 Expression.Or(Expression.Invoke(result, parameter), Expression.Invoke(expressions[i], parameter)),
        //                 parameter) : Expression.Lambda<Func<T, bool>>(
        //                 Expression.And(Expression.Invoke(result, parameter), Expression.Invoke(expressions[i], parameter)),
        //                 parameter);
        //         }
        //     }

        //     result = Expression.Lambda<Func<T, bool>>(
        //         Expression.And(Expression.Invoke(notDeletedExpression, parameter), Expression.Invoke(result, parameter)),
        //         parameter);

        //     return result;
        // }

        // private Expression<Func<T, bool>> GetNotDeletedExpression(ParameterExpression entity)
        // {
        //     var statement = Expression.Equal(Expression.Property(entity, Constants.IS_DELETED), Expression.Constant(false));
        //     return Expression.Lambda<Func<T, bool>>(statement, entity);
        // }

        // private Expression GetPropertyExpression(string filterName, ParameterExpression entity)
        // {
        //     const string PATH_SEPARATOR = ".";
        //     Expression result;
        //     if (filterName.Contains(PATH_SEPARATOR))
        //     {
        //         Expression relatedEntity = null;
        //         var tokens = filterName.Split(PATH_SEPARATOR);
        //         for (int i = 0; i < tokens.Length - 1; i++)
        //         {
        //             relatedEntity = Expression.Property(relatedEntity ?? entity, tokens[i]);
        //         }
        //         result = Expression.Property(relatedEntity, tokens[tokens.Length - 1]);
        //     }
        //     else
        //     {
        //         result = Expression.Property(entity, filterName);
        //     }
        //     return result;
        // }

        // private Expression GetStringContainsExpression(Expression property, Expression constant)
        // {
        //     return Expression.Call(property, typeof(string).GetMethod(Constants.CONTAINS, new[] { typeof(string) }), constant);
        // }

        // private Expression GetStartEndDateTimeExpression(string filterName, Expression property, Expression constant)
        // {
        //     const string START = "start";
        //     const string END = "end";

        //     Expression result = null;

        //     if (filterName.Contains(START, StringComparison.InvariantCultureIgnoreCase))
        //     {
        //         result = Expression.GreaterThanOrEqual(property, constant);
        //     }
        //     else if (filterName.Contains(END, StringComparison.InvariantCultureIgnoreCase))
        //     {
        //         result = Expression.LessThanOrEqual(property, constant);
        //     }
        //     return result;
        // }

        // private Expression<Func<T, bool>> GetStatementExpression(string filterName, object filterValue,
        //                                                 ParameterExpression entity, Expression property, Expression constant)
        // {
        //     Expression statement = null;

        //     if (filterValue is string)
        //     {
        //         statement = GetStringContainsExpression(property, constant);
        //     }
        //     else if (filterValue is DateTime)
        //     {
        //         statement = GetStartEndDateTimeExpression(filterName, property, constant);
        //     }
        //     else
        //     {
        //         statement = Expression.Equal(property, constant);
        //     }
        //     return Expression.Lambda<Func<T, bool>>(statement, entity);
        // }

        // private Expression<Func<T, bool>> GetDatabaseQueryExpression(List<Expression<Func<T, bool>>> expressions,
        //                                                              ParameterExpression entity)
        // {
        //     var result = expressions[0];

        //     if (expressions.Count > 1)
        //     {
        //         for (int i = 1; i < expressions.Count; i++)
        //         {
        //             result = Expression.Lambda<Func<T, bool>>(
        //                 Expression.And(Expression.Invoke(result, entity), Expression.Invoke(expressions[i], entity)),
        //                 entity);
        //         }
        //     }
        //     return result;
        // }
        // public Expression<Func<T, bool>> Filter(IDictionary<string, object> filters)
        // {
        //     var expressions = new List<Expression<Func<T, bool>>>();

        //     var entity = Expression.Parameter(typeof(T), Constants.ENTITY);

        //     var notDeletedExpression = GetNotDeletedExpression(entity);

        //     if (filters == null || !filters.Any()) return notDeletedExpression;

        //     foreach (var filterName in filters.Keys)
        //     {
        //         var filterValue = filters[filterName];
        //         var constant = Expression.Constant(filterValue);
        //         var property = GetPropertyExpression(filterName, entity);
        //         expressions.Add(GetStatementExpression(filterName, filterValue, entity, property, constant));
        //     }

        //     var databaseQuery = GetDatabaseQueryExpression(expressions, entity);

        //     return Expression.Lambda<Func<T, bool>>(
        //         Expression.And(Expression.Invoke(notDeletedExpression, entity), Expression.Invoke(databaseQuery, entity)),
        //         entity);
        // }

        public ApplicationDbContext GetContext()
        {
            return _context;
        }

        public Expression<Func<T, object>> SortBy(string sort) => Activator.CreateInstance<T>().SortBy(sort) as Expression<Func<T, object>>;

        public async Task<T> Find(Guid? id)
        {
            var whereClause = new ExpressionBuilder<T>().Where(nameof(Basic.IsDeleted), Operator.EQUALS, false)
                                                        .Build();

            return await _context
                .Set<T>()
                .Where(whereClause)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public bool Exists(Guid id)
        {
            var whereClause = new ExpressionBuilder<T>().Where(nameof(Basic.IsDeleted), Operator.EQUALS, false)
                                                        .Build();
            return _context
                .Set<T>()
                .Where(whereClause)
                .Any(a => a.Id == id);
        }

        public async Task<List<T>> Search(int page = 1, string filter = "", string sort = "", string order = Constants.ASC)
        {
            var whereClause = new ExpressionBuilder<T>().Where(nameof(Basic.SearchFields), Operator.LIKE, filter)
                                                        .And(nameof(Basic.IsDeleted), Operator.EQUALS, false)
                                                        .Build();
            var orderBy = SortBy(sort);

            var query = _context
                .Set<T>()
                .Where(whereClause);

            foreach (var item in Activator.CreateInstance<T>().GetRelatedItems())
            {
                query = query.Include(item.Name);
            }

            if (order == Constants.ASC)
            {
                query = query.OrderBy(orderBy);
            }
            else
            {
                query = query.OrderByDescending(orderBy);
            }

            return await query
                .Skip((page - 1) * Constants.MAX_ITEMS_PER_PAGE)
                .Take(Constants.MAX_ITEMS_PER_PAGE)
                .ToListAsync();
        }

        public async Task<int> Count(string filter = "")
        {
            var whereClause = new ExpressionBuilder<T>().Where(nameof(Basic.SearchFields), Operator.LIKE, filter)
                                                        .And(nameof(Basic.IsDeleted), Operator.EQUALS, false)
                                                        .Build();
            return await _context
                .Set<T>()
                .Where(whereClause)
                .Select(a => a.Id)
                .CountAsync();
        }
        public async Task<T> Create(T data)
        {
            _context.Add(data);
            LoadRelatedItems(data);
            data.CreateSearchText();
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<T> Edit(Guid id, T data)
        {
            try
            {
                _context.Update(data);
                LoadRelatedItems(data);
                data.CreateSearchText();
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return data;
        }

        public async Task<int> Delete(T data)
        {
            _context.Update(data);
            return await _context.SaveChangesAsync();
        }

        private void LoadRelatedItems(T data)
        {
            foreach (var item in data.GetRelatedItems())
            {
                if (typeof(IEnumerable).IsAssignableFrom(item.Type))
                {
                    _context.Entry(data).Collection(item.Name).Load();
                }
                else
                {
                    _context.Entry(data).Reference(item.Name).Load();
                }
            }
        }
    }
}
