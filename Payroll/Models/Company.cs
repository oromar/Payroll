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
    }
}
