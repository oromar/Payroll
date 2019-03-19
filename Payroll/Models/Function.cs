using MMLib.Extensions;
using Payroll.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Payroll.Models
{
    public class Function: Basic
    {
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "Company")]
        public Guid CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Description")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string Description { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "IsManagerFunction")]
        public bool IsManagerFunction { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "ManagerCommission")]
        public double ManagerCommission { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "HasDangeroues")]
        public bool HasDangerous { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "HasUnhealthy")]
        public bool HasUnhealthy { get; set; }

        public override void CreateSearchText()
        {
            SearchFields = $@"{Name} {Description} {ManagerCommission} {CreatedBy}".RemoveDiacritics();
        }

        public override List<string> GetSearchFields()
        {
            return new List<string>
            {
                Resource.Name,
                Resource.Description,
                Resource.ManagerCommission,
                Resource.CreatedBy
            };
        }

        public override Expression SortBy(string sort)
        {
            Expression<Func<Function, object>> result = null;

            switch (sort)
            {
                case Constants.SORT_COMPANY_NAME:
                    result = a => a.Company.Name;
                    break;

                case Constants.SORT_MANAGER_COMMISSION:
                    result = a => a.ManagerCommission;
                    break;

                case Constants.SORT_DESCRIPTION:
                    result = a => a.Description;
                    break;

                case Constants.SORT_CREATED_BY:
                    result= a => a.CreatedBy;
                    break;

                default:
                    result = a => a.Name;
                    break;
            }

            return result as Expression;
        }
    }
}
