using Microsoft.EntityFrameworkCore;
using MMLib.Extensions;
using Payroll.Common;
using Payroll.Data;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Payroll.Business
{
    public class BusinessObject<T> where T : Basic
    {
        protected readonly ApplicationDbContext _context;

        public BusinessObject(ApplicationDbContext context)
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
            

        public Expression<Func<T, object>> SortBy(string sort)
        {
            if (string.IsNullOrEmpty(sort))
            {
                return a => a.Name;
            }

            return a => a.GetType()
                         .GetProperty(sort)
                         .GetValue(a);
        }

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

        public async Task<T> Create(T data, string userIdentity)
        {
            data.Id = Guid.NewGuid();
            data.CreatedAt = DateTime.Now;
            data.CreatedBy = userIdentity;
            HandleSearchFields(data);
            data.IsDeleted = false;
            _context.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<T> Edit(Guid id, T data, string userIdentity)
        {
            try
            {
                data.UpdatedAt = DateTime.Now;
                data.UpdatedBy = userIdentity;
                HandleSearchFields(data);
                _context.Update(data);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return data;
        }

        public async Task<int> Delete(Guid id, string userIdentity)
        {
            var data = await Find(id);
            data.IsDeleted = true;
            data.DeletedBy = userIdentity;
            data.DeletedAt = DateTime.Now;
            _context.Update(data);
            return await _context.SaveChangesAsync();
        }

        private static void HandleSearchFields(T data)
        {
            var searchableTypes = new[] { typeof(string), typeof(int), typeof(double), typeof(decimal), typeof(float) };

            data.SearchFields = data.GetType()
                                    .GetProperties()
                                    .Where(a => searchableTypes.Contains(a.PropertyType))
                                    .Where(a => a.GetValue(data) != null)
                                    .Select(a => a.GetValue(data).ToString().RemoveDiacritics().Trim())
                                    .Aggregate((a, b) => a + " " + b);
        }
    }
}
