using Payroll.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Payroll.Models
{
    public class WorkHours : Basic
    {
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "Company")]
        public Guid CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        public IEnumerable<WorkHourItem> WorkHourItems { get; set; }

        public virtual IEnumerable<WorkHoursEmployee> Employees { get; set; }

        public override Expression SortBy(string sort)
        {
            Expression<Func<WorkHours, object>> result = null;

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
