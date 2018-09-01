using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMLib.Extensions;
using Payroll.Common;
using Payroll.Data;
using Payroll.Models;

namespace Payroll.Business
{
    public class CurrenciesBO: GenericBO<Currency>
    {
        public CurrenciesBO(ApplicationDbContext context): base(context) {}

        public override Expression<Func<Currency, object>> GetOrder()
        {
            return a => a.Name;
        }

        public override IQueryable<Currency> BaseQuery(string filter)
        {
            return _context.Currency
                 .Where(a => !a.Deleted)
                 .Where(a => string.IsNullOrEmpty(filter) ||
                    a.Name.RemoveDiacritics().Contains(filter.RemoveDiacritics(), 
                        StringComparison.InvariantCultureIgnoreCase));
        }

        public override async Task<Currency> Find(Guid? id)
        {
            var currency = await _context.Currency
             .Where(a => !a.Deleted)
             .FirstOrDefaultAsync(m => m.Id == id);
            return currency;
        }
    }
}
