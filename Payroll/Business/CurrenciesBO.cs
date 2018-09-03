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

        public override Expression<Func<Currency, object>> OrderBy() 
            => a => a.Name;

        public override Expression<Func<Currency, bool>> FilterBy(string filter) 
            => a => !a.IsDeleted &&
            (string.IsNullOrEmpty(filter) || 
                a.Name.RemoveDiacritics()
                    .Contains(filter.RemoveDiacritics(), StringComparison.InvariantCultureIgnoreCase));
    }
}
