using MMLib.Extensions;
using Payroll.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Payroll.Models
{
    public class Currency: Basic
    {
        [Display(ResourceType = typeof(Resource), Name = "Exchange")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName ="RequiredField")]
        public double Exchange { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Symbol")]
        public string Symbol { get; set; }

        public override void CreateSearchText()
        {
            SearchFields = $@"{Name} 
                              {Symbol} 
                              {Exchange} 
                              {CreatedBy}"
                              .RemoveDiacritics();
        }

        public override List<string> GetSearchFields()
        {
            return new List<string>
            {
                Resource.Name,
                Resource.Symbol,
                Resource.Exchange,
                Resource.CreatedBy
            };
        }

        public override Expression SortBy(string sort)
        {
            Expression<Func<Currency, object>> result = null;

            switch(sort)
            {
                case Constants.SORT_EXCHANGE:
                    result = a => a.Exchange;
                    break;

                case Constants.SORT_SYMBOL:
                    result = a => a.Symbol;
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
