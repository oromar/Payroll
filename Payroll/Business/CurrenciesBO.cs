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
    public class CurrenciesBO: GenericBO<Currency>
    {

        public CurrenciesBO(ApplicationDbContext context): base(context) { }

        public override async Task<int> Count(int page = 1, string filter = "")
        {
            return await _context.Currency
                 .Where(a => !a.Deleted)
                 .Where(a => string.IsNullOrEmpty(filter) || a.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase))
                 .CountAsync();
        }

        public override async Task<List<Currency>> Search(int page = 1, string filter = "")
        {
            return await _context.Currency
                .Where(a => !a.Deleted)
                .Where(a => string.IsNullOrEmpty(filter) || a.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(a => a.Name)
                .Skip((page - 1) * Constants.MAX_ITEMS_PER_PAGE)
                .Take(Constants.MAX_ITEMS_PER_PAGE)
                .ToListAsync();
        }

        public override async Task<Currency> Details(Guid id)
        {
            var currency = await _context.Currency
                .Where(a => !a.Deleted)
                .FirstOrDefaultAsync(m => m.Id == id);

            return currency;
        }

        public override async Task<Currency> Find(Guid? id)
        {
            return await _context.Currency.FindAsync(id);
        }

        public override bool Exists(Guid id)
        {
            return _context.Currency.Any(e => e.Id == id);
        }
    }
}
