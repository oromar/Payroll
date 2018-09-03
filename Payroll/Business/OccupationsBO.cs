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

        public override Expression<Func<Occupation, object>> OrderBy(string sort)
        {
            if (string.IsNullOrEmpty(sort))
            {
                return a => a.Name;
            } 
            else
            {
                switch(sort)
                {
                    case "Name":
                        return a => a.Name;
                    case "CouncilName":
                        return a => a.CouncilName;
                    case "CreatedBy":
                        return a => a.CreatedBy;
                }
            }
            return null;
        }
            

        public override Expression<Func<Occupation, bool>> FilterBy(string filter) 
            => a => !a.IsDeleted &&
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
