using MMLib.Extensions;
using Payroll.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Payroll.Models
{
    public class Workplace: Addressable
    {
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Company")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public Guid CompanyId { get; set; }

        public override void CreateSearchText()
        {
            SearchFields = $@"{Company.Name} {Name} {Address} {City} {State} {CreatedBy}".RemoveDiacritics();
        }

        public override List<string> GetSearchFields()
        {
            return new List<string>
            {
                Resource.Company,
                Resource.Name,
                Resource.Address,
                Resource.City,
                Resource.State,
                Resource.CreatedBy
            };
        }

        public override Expression SortBy(string sort)
        {
            Expression<Func<Workplace, object>> result = null;

            switch (sort)
            {
                case Constants.SORT_COMPANY_NAME:
                    result = a => a.Company.Name;
                    break;

                case Constants.SORT_ADDRESS:
                    result = a => a.Address;
                    break;

                case Constants.SORT_CITY:
                    result = a => a.City;
                    break;

                case Constants.SORT_STATE:
                    result = a => a.State;
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
