using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payroll.Business;
using Payroll.Common;
using Payroll.Data;
using Payroll.Models;
namespace Payroll.Business
{
    public class CurrenciesBO
    {
        private readonly ApplicationDbContext _context;

        public CurrenciesBO(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HasMore(int page = 1, string filter = "")
        {
            return await _context.Currency
                 .Where(a => !a.Deleted)
                 .Where(a => string.IsNullOrEmpty(filter) || a.Name.Contains(filter))
                 .CountAsync() > (page * Constants.MAX_ITEMS_PER_PAGE);
        }

        public async Task<List<Currency>> Search(int page = 1, string filter = "")
        {
            return await _context.Currency
                .Where(a => !a.Deleted)
                .Where(a => string.IsNullOrEmpty(filter) || a.Name.Contains(filter))
                .OrderBy(a => a.Name)
                .Skip((page - 1) * Constants.MAX_ITEMS_PER_PAGE)
                .Take(Constants.MAX_ITEMS_PER_PAGE)
                .ToListAsync();
        }

        public async Task<Currency> Details(Guid id)
        {
            var currency = await _context.Currency
                .Where(a => !a.Deleted)
                .FirstOrDefaultAsync(m => m.Id == id);

            return currency;
        }

        public async Task<Currency> Create(Currency currency, string userIdentity)
        {
            currency.Id = Guid.NewGuid();
            currency.CreationTime = DateTime.Now;
            currency.CreationUser = userIdentity;
            currency.Deleted = false;
            _context.Add(currency);
            await _context.SaveChangesAsync();
            return currency;
        }

        public async Task<Currency> Find(Guid? id)
        {
            return await _context.Currency.FindAsync(id);
        }

        public async Task<Currency> Edit(Guid id, Currency currency, string userIdentity)
        {
            try
            {
                currency.LastUpdateTime = DateTime.Now;
                currency.LastUpdateUser = userIdentity;
                _context.Update(currency);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return currency;
        }

        public async Task<int> Delete(Guid id, string userIdentity)
        {
            var currency = await _context.Currency.FindAsync(id);
            currency.Deleted = true;
            currency.DeleteUser = userIdentity;
            currency.DeleteTime = DateTime.Now;
            _context.Update(currency);
            return await _context.SaveChangesAsync();
        }

        public bool CurrencyExists(Guid id)
        {
            return _context.Currency.Any(e => e.Id == id);
        }
    }
}
