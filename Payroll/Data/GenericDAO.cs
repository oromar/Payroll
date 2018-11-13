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

        public Expression<Func<T, bool>> FilterBy(string filter)
        {
            var parameter = Expression.Parameter(typeof(T), "entity");

            var isDeletedProperty = Expression.Property(parameter, "IsDeleted");
            var falseConstant = Expression.Constant(false);
            var notDeletedMethod = Expression.Call(isDeletedProperty, typeof(Boolean).GetMethod("Equals", new[] { typeof(Boolean) }), falseConstant);
            var notDeletedExpression = Expression.Lambda<Func<T, bool>>(notDeletedMethod, parameter);
            if (filter.IsNullOrEmpty()) return notDeletedExpression;

            var normalizedFilter = filter.RemoveDiacritics().Trim();
            var propertyName = Expression.Property(parameter, "SearchFields");
            var constantParameter = Expression.Constant(normalizedFilter);
            var containsMethod = Expression.Call(propertyName, typeof(string).GetMethod("Contains", new[] { typeof(string) }), constantParameter);
            var containsExpression = Expression.Lambda<Func<T, bool>>(containsMethod, parameter);
            var body = Expression.And(Expression.Invoke(notDeletedExpression, parameter), Expression.Invoke(containsExpression, parameter));
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public ApplicationDbContext GetContext()
        {
            return _context;
        }

        public Expression<Func<T, object>> SortBy(string sort) => Activator.CreateInstance<T>().SortBy(sort) as Expression<Func<T, object>>;

        public async Task<T> Find(Guid? id)
        {
            return await _context
                .Set<T>()
                .Where(FilterBy(null))
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public bool Exists(Guid id)
        {
            return _context
                .Set<T>()
                .Where(FilterBy(null))
                .Any(a => a.Id == id);
        }

        public async Task<List<T>> Search(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
            var where = FilterBy(filter);
            var orderBy = SortBy(sort);

            var query = _context
                .Set<T>()
                .Where(where);

            HandleRelatedItems((item) =>
            {
                query = query.Include(item.Name);
            });

            if (order == "ASC")
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
            return await _context
                .Set<T>()
                .Where(FilterBy(filter))
                .CountAsync();
        }

        public async Task<T> Details(Guid id)
        {
            return await Find(id);
        }

        public async Task<T> Create(T data)
        {
            _context.Add(data);

            LoadRelatedItems(data);

            HandleSearchFields(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<T> Edit(Guid id, T data)
        {
            try
            {
                _context.Update(data);
                LoadRelatedItems(data);
                HandleSearchFields(data);                                
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
            HandleRelatedItems((item) =>
            {
                if (typeof(IEnumerable).IsAssignableFrom(item.PropertyType))
                {
                    _context.Entry(data).Collection(item.Name).Load();
                }
                else
                {
                    _context.Entry(data).Reference(item.Name).Load();
                }
            });
        }

        private void HandleRelatedItems(Action<PropertyInfo> action)
        {
            var related = Activator.CreateInstance<T>()
                                   .GetType()
                                   .GetProperties()
                                   .Where(a => a.PropertyType.BaseType != null)
                                   .Where(a => a.PropertyType.BaseType.Name == nameof(Basic) ||
                                               a.PropertyType.BaseType.Name == nameof(Addressable))
                                   .ToList();

            if (related != null && related.Any())
            {
                foreach (var item in related)
                {
                    action(item);
                }
            }
        }

        private static void HandleSearchFields(T data)
        {
            var searchValues = new List<string>();

            var types = new[] { typeof(string), typeof(int), typeof(double), typeof(decimal), typeof(float), typeof(Common.DayOfWeek) };

            var relatedTypes = new[] { nameof(Basic), nameof(Addressable) };

            var baseProperties = data.GetType()
                .GetProperties();

            var relatedEntities = baseProperties
                .Where(a => a.PropertyType.BaseType != null)
                .Where(a => relatedTypes.Contains(a.PropertyType.BaseType.Name))
                .ToList();

            var fields = baseProperties
                .Where(a => types.Contains(a.PropertyType))
                .Where(a => a.GetValue(data) != null)
                .Select(a => a.GetValue(data).ToString().RemoveDiacritics().Trim());

            searchValues.AddRange(fields);

            foreach (var entity in relatedEntities)
            {
                var entityProperties = entity.PropertyType
                    .GetProperties()
                    .Where(a => types.Contains(a.PropertyType))
                    .ToList();

                var relatedObject = data.GetPropertyValue(data.GetType().GetProperty(entity.Name).Name);

                if (relatedObject != null)
                {
                    var value = relatedObject.GetPropertyValue<string>("Name");

                    if (value != null)
                    {
                        searchValues.Add(value.ToString().RemoveDiacritics().Trim());
                    }
                }
            }
            data.SearchFields = searchValues.Aggregate((a, b) => a + " " + b);
        }
    }
}
