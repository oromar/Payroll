using MMLib.Extensions;
using Payroll.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Payroll.Models
{
    public class Company : Addressable
    {
        [Display(ResourceType = typeof(Resource), Name = "PersonalJuridicalName")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string PersonalJuridicalName { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "SocialReason")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string SocialReason { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "OccupationArea")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string OccupationArea { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "FoundationDate")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public DateTime? FoundationDate { get; set; }
        [Display(ResourceType = typeof(Resource), Name = nameof(Nacionality))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.RequiredField))]
        public string Nacionality { get; set; }
        [Display(ResourceType = typeof(Resource), Name = nameof(HasStrangers))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.RequiredField))]
        public bool HasStrangers { get; set; }
        [Display(ResourceType = typeof(Resource), Name = nameof(Currency))]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = nameof(Resource.RequiredField))]
        public Guid CurrencyId { get; set; }
        [ForeignKey(nameof(CurrencyId))]
        public virtual Currency PaymentCurrency { get; set; }
        public virtual IEnumerable<Workplace> Workplaces { get; set; }
        public virtual IEnumerable<JobRole> JobRoles { get; set; }
        public virtual IEnumerable<Department> Departments { get; set; }

        public override List<string> GetSearchFields()
        {
            return new List<string>
            {
                Resource.Name,
                Resource.PersonalJuridicalName,
                Resource.SocialReason,
                Resource.OccupationArea,
                Resource.Currency,
                Resource.CreatedBy,
            };
        }

        public override void CreateSearchText()
        {
            SearchFields = string.Join(" ", Name,
                                            PersonalJuridicalName,
                                            SocialReason,
                                            OccupationArea,
                                            PaymentCurrency.Name,
                                            CreatedBy)
                                 .RemoveDiacritics();
        }

        public override Expression SortBy(string sort)
        {
            Expression<Func<Company, object>> result = null;

            switch (sort)
            {
                case Constants.SORT_OCCUPATION_AREA:
                    result = a => a.OccupationArea;
                    break;

                case Constants.SORT_SOCIAL_REASON:
                    result = a => a.SocialReason;
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
