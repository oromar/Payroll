using Microsoft.AspNetCore.WebSockets.Internal;
using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public abstract Task<T> Find(Guid? id);

        public abstract Task<int> Count(int page = 1, string filter = "");

        public abstract Task<List<T>> Search(int page = 1, string filter = "");

        public abstract Task<T> Details(Guid id);

        public abstract bool Exists(Guid id);

        public async Task<T> Create(T data, string userIdentity)
        {
            data.Id = Guid.NewGuid();
            data.CreationTime = DateTime.Now;
            data.CreationUser = userIdentity;
            data.Deleted = false;
            _context.Add(data);
            await _context.SaveChangesAsync();
            return data;
        }

        public async Task<T> Edit(Guid id, T data, string userIdentity)
        {
            try
            {
                data.LastUpdateTime = DateTime.Now;
                data.LastUpdateUser = userIdentity;
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
            data.Deleted = true;
            data.DeleteUser = userIdentity;
            data.DeleteTime = DateTime.Now;
            _context.Update(data);
            return await _context.SaveChangesAsync();
        }
    }
}
