using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Models
{
    public class Company: Addressable
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
        [Display(ResourceType = typeof(Resource), Name = "Nacionality")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string Nacionality { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "HasStrangers")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public bool HasStrangers { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Currency")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public Guid CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public virtual Currency PaymentCurrency { get; set; }
        public virtual IEnumerable<Workplace> Workplaces { get; set; }
        public virtual IEnumerable<JobRole> JobRoles { get; set; }
    }
}
