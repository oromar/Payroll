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

        public async Task<bool> Exists(Guid id)
        {
            var whereClause = new ExpressionBuilder<T>().Where(nameof(Basic.IsDeleted), Operator.EQUALS, false)
                                                        .Build();
            return await _context
                .Set<T>()
                .Where(whereClause)
                .AnyAsync(a => a.Id == id);
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
