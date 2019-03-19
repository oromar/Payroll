using MMLib.Extensions;
using Payroll.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class Department : Basic
    {
        [Display(ResourceType = typeof(Resource), Name = "Company")]
        public Guid CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Description")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string Description { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "IsManagerDepartment")]
        public bool IsManagerDepartment { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "IsOperationalDepartment")]
        public bool IsOperationalDepartment { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "IsDirectionDepartment")]
        public bool IsDirectionDepartment { get; set; }

        public virtual IEnumerable<Employee> Employees { get; set; }

        public override void CreateSearchText()
        {
            SearchFields = string.Join(" ", Name,
                                            Description,
                                            CreatedBy)
                                 .RemoveDiacritics();
        }

        public override List<string> GetSearchFields()
        {
            return new List<string>
            {
                Resource.Name,
                Resource.Description,
                Resource.CreatedBy
            };
        }

        public override Expression SortBy(string sort)
        {
            Expression<Func<Department, object>> result = null;

            switch (sort)
            {
                case Constants.SORT_CREATED_BY:
                    result = a => a.CreatedBy;
                    break;
                case Constants.SORT_DESCRIPTION:
                    result = a => a.Description;
                    break;
                default:
                    result = a => a.Name;
                    break;
            }
            return result as Expression;
        }
    }
}
