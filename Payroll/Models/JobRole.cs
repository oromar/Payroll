using Payroll.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Payroll.Models
{
    public class JobRole: Basic
    {
        [Display(ResourceType = typeof(Resource), Name = "Description")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string Description { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Company")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public Guid CompanyId { get; set; }

        public override Expression SortBy(string sort)
        {
            Expression<Func<JobRole, object>> result = null;

            switch(sort)
            {
                case Constants.SORT_COMPANY_NAME:
                    result = a => a.Company.Name;
                    break;
                case Constants.SORT_DESCRIPTION:
                    result = a => a.Description;
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
