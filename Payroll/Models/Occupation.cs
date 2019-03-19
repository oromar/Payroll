using MMLib.Extensions;
using Payroll.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Payroll.Models
{
    public class Occupation : Basic
    {
        [Display(ResourceType = typeof(Resource), Name = "HasCouncil")]
        public bool IsRegulated { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "CouncilName")]
        public string CouncilName { get; set; }

        public override void CreateSearchText()
        {
            SearchFields = $@"{Name} {CouncilName} {CreatedBy}".RemoveDiacritics();
        }

        public override List<string> GetSearchFields()
        {
            return new List<string> 
            {
                Resource.Name,
                Resource.CouncilName,
                Resource.CreatedBy
            };
        }

        public override Expression SortBy(string sort)
        {
            Expression<Func<Occupation, object>> result = null;

            switch (sort)
            {
                case Constants.SORT_CREATED_BY:
                    result = a => a.CreatedBy;
                    break;
                case Constants.SORT_COUNCIL_NAME:
                    result = a => a.CouncilName;
                    break;

                default:
                    result = a => a.Name;
                    break;
            }
            return result as Expression;
        }
    }
}
