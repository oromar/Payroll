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

        public override Expression<Func<Currency, object>> OrderBy(string sort)
        {
            if (string.IsNullOrEmpty(sort))
            {
                return a => a.Name;
            }
            else
            {
                switch (sort)
                {
                    case "Name":
                        return a => a.Name;
                    case "Symbol":
                        return a => a.Symbol;
                    case "Exchange":
                        return a => a.Exchange;
                    case "CreatedBy":
                        return a => a.CreatedBy;
                }
            }

            return null;
        }

        public override Expression<Func<Currency, bool>> FilterBy(string filter) 
            => a => !a.IsDeleted &&
            (string.IsNullOrEmpty(filter) || 
                a.Name.RemoveDiacritics()
                    .Contains(filter.RemoveDiacritics(), StringComparison.InvariantCultureIgnoreCase));
    }
}
