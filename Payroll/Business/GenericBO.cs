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
    public abstract class GenericBO<T> where T : Basic
    {
        protected readonly ApplicationDbContext _context;

        public GenericBO(ApplicationDbContext context)
        {
            _context = context;
        }

        public Expression<Func<T, bool>> FilterBy(string filter) =>
            a => !a.IsDeleted && 
                    a.SearchFields.RemoveDiacritics()
                            .Contains(filter.RemoveDiacritics(), StringComparison.InvariantCultureIgnoreCase);

        public Expression<Func<T, object>> SortBy(string sort)
        {
            if (string.IsNullOrEmpty(sort))
            {
                return a => a.Id;
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
            var query = _context
                .Set<T>()
                .Where(FilterBy(filter));

            if (order == "ASC")
            { 
                query = query.OrderBy(SortBy(sort));
            } 
            else
            {
                query = query.OrderByDescending(SortBy(sort));
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
            data.SearchFields = data.GetType()
                                    .GetProperties()
                                    .ToList()
                                    .Where(a => a.PropertyType == typeof(string))
                                    .Where(a => a.GetValue(data) != null)
                                    .Select(a => a.GetValue(data).ToString())
                                    .Aggregate((a, b) => a + "," + b);
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
                data.SearchFields = data.GetType()
                                        .GetProperties()
                                        .ToList()
                                        .Where(a => a.GetValue(data) != null)
                                        .Where(a => a.PropertyType == typeof(string))
                                        .Select(a => a.GetValue(data).ToString())
                                        .Aggregate((a, b) => a + "," + b);
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
    }
}
