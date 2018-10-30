using Payroll.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Payroll.Models
{
    public class Vacation : Basic
    {
        public Guid CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company Company { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual IEnumerable<VacationEmployee> Employees { get; set; }

        public override Expression SortBy(string sort)
        {
            Expression<Func<Vacation, object>> result = null;

            switch (sort)
            {
                case Constants.SORT_COMPANY_NAME:
                    result = a => a.Company.Name;
                    break;

                case Constants.SORT_CREATED_BY:
                    result = a => a.CreatedBy;
                    break;

                default:
                    result = a => a.Name;
                    break;
            }

            return result as Expression;
        }
    }
}
