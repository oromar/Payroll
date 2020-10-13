using ExpressionUtils;
using Microsoft.EntityFrameworkCore;
using Payroll.Common;
using Payroll.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
namespace Payroll.Data
{
    public class GenericDAO<T> where T : Basic
    {
        public GenericDAO(ApplicationDbContext context)
        {
            Context = context;
        }

        public ApplicationDbContext Context { get; }

        public Expression<Func<T, object>> SortBy(string sort) => Activator.CreateInstance<T>().SortBy<T>(sort);

        public async Task<T> Find(Guid? id)
        {
            var filter = new ExpressionBuilder<T>().Where(nameof(Basic.IsDeleted), Operator.EQUALS, false)
                                                   .And(a => a.Id == id)
                                                   .Build();

            var result =  await Context
                .Set<T>()
                .FirstOrDefaultAsync(filter);

            await LoadRelatedItems(result);

            return result;
        }

        public async Task<bool> Exists(Guid id)
        {
            var filter = new ExpressionBuilder<T>().Where(nameof(Basic.IsDeleted), Operator.EQUALS, false)
                                                   .And(nameof(Basic.Id), Operator.EQUALS, id)
                                                   .Build();
            return await Context
                .Set<T>()
                .AnyAsync(filter);
        }

        public async Task<List<T>> Search(int page = 1, string filter = "", string sort = "", string order = Constants.ASC)
        {
            var filterInDB = new ExpressionBuilder<T>().Where(nameof(Basic.IsDeleted), Operator.EQUALS, false)
                                                       .And(nameof(Basic.SearchText), Operator.ILIKE, filter)
                                                       .Build();
            var orderBy = SortBy(sort);

            var query = Context
                .Set<T>()
                .Where(filterInDB);

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

            var result = await query
                .Skip((page - 1) * Constants.MAX_ITEMS_PER_PAGE)
                .Take(Constants.MAX_ITEMS_PER_PAGE)
                .ToListAsync();

            result.ForEach(a => LoadRelatedItems(a).Wait());

            return result;
        }

        public async Task<int> Count(string filter = "")
        {
            var filterInDB = new ExpressionBuilder<T>().Where(nameof(Basic.IsDeleted), Operator.EQUALS, false)
                                                       .And(nameof(Basic.SearchText), Operator.LIKE, filter)
                                                       .Build();
            return await Context
                .Set<T>()
                .CountAsync(filterInDB);
        }
        public async Task<T> Create(T data)
        {
            Context.Add(data);
            await LoadRelatedItems(data);
            await Context.SaveChangesAsync();
            return data;
        }

        public async Task<T> Edit(Guid id, T data)
        {
            Context.Update(data);
            await LoadRelatedItems(data);
            await Context.SaveChangesAsync();
            return data;
        }

        public async Task<int> Delete(T data)
        {
            Context.Update(data);
            return await Context.SaveChangesAsync();
        }

        private async Task LoadRelatedItems(T data)
        {
            foreach (var item in data.GetRelatedItems())
            {
                if (typeof(IEnumerable).IsAssignableFrom(item.Type))
                {
                    await Context.Entry(data).Collection(item.Name).LoadAsync();
                }
                else
                {
                    await Context.Entry(data).Reference(item.Name).LoadAsync();
                }
            }
        }
    }
}
