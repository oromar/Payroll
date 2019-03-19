using MMLib.Extensions;
using Payroll.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Payroll.Models
{
    public class LicenseType : Basic
    {

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName ="RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "Description")]
        public string Description { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "QtyDaysDefault")]
        public int QtyDaysDefault { get; set; }

        public override void CreateSearchText()
        {
            SearchFields = $@"{Name} {Description} {QtyDaysDefault} {CreatedBy}".RemoveDiacritics();
        }

        public override List<string> GetSearchFields()
        {
            return new List<string>
            {
                Resource.Name,
                Resource.Description,
                Resource.QtyDaysDefault,
                Resource.CreatedBy
            };
        }

        public override Expression SortBy(string sort)
        {
            Expression<Func<LicenseType, object>> result = null;

            switch (sort)
            {
                case Constants.SORT_QTY_DAYS:
                    result = a => a.QtyDaysDefault;
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
