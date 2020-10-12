using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payroll.Business
{
    public class BusinessObject<T> where T : Basic
    {
        protected readonly GenericDAO<T> _dao;

        public BusinessObject(GenericDAO<T> dao)
        {
            _dao = dao;
        }

        public GenericDAO<T> GetDAO()
        {
            return _dao;
        }

        public virtual async Task<T> Find(Guid? id)
        {
            return await _dao.Find(id);
        }

        public virtual async Task<bool> Exists(Guid id)
        {
            return await _dao.Exists(id);
        }

        public virtual async Task<List<T>> Search(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
            return await _dao.Search(page, filter, sort, order);
        }

        public virtual async Task<int> Count(string filter = "")
        {
            return await _dao.Count(filter);
        }

        public virtual async Task<T> Create(T data, string userIdentity)
        {
            data.Id = Guid.NewGuid();
            data.CreatedAt = DateTime.Now;
            data.CreatedBy = userIdentity;
            data.IsDeleted = false;
            return await _dao.Create(data);
        }

        public virtual async Task<T> Edit(Guid id, T data, string userIdentity)
        {
            try
            {
                data.UpdatedAt = DateTime.Now;
                data.UpdatedBy = userIdentity;
                await _dao.Edit(id, data);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return data;
        }

        public virtual async Task<int> Delete(Guid id, string userIdentity)
        {
            var data = await Find(id);
            data.IsDeleted = true;
            data.DeletedBy = userIdentity;
            data.DeletedAt = DateTime.Now;
            return await _dao.Delete(data);
        }
    }
}

