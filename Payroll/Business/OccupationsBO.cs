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
    public class OccupationsBO: GenericBO<Occupation>
    {
        public OccupationsBO(ApplicationDbContext context): base(context) {}

        public override Expression<Func<Occupation, object>> OrderBy() 
            => a => a.Name;

        public override Expression<Func<Occupation, bool>> FilterBy(string filter) 
            => a => !a.Deleted &&
                (string.IsNullOrEmpty(filter) || 
                    (a.Name.RemoveDiacritics().
                        Contains(filter.RemoveDiacritics(), 
                            StringComparison.InvariantCultureIgnoreCase) ||
                            (a.CouncilName != null &&
                                a.CouncilName.RemoveDiacritics().
                                    Contains(filter.RemoveDiacritics(), 
                                        StringComparison.InvariantCultureIgnoreCase))));
    }
}
