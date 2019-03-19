using MMLib.Extensions;
using Payroll.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Payroll.Models
{
    public class OccurrenceType: Basic
    {
        [Display(ResourceType = typeof(Resource), Name = "Description")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string Description { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "IsAbsence")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public bool IsAbsence { get; set; }

        public override void CreateSearchText()
        {
            SearchFields = $@"{Name} {Description} {CreatedBy}".RemoveDiacritics();
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
            Expression<Func<OccurrenceType, object>> result = null;

            switch (sort)
            {
                case Constants.SORT_IS_ABSENCE:
                    result = a => a.IsAbsence;
                    break;

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
