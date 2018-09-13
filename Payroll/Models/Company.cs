using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Models
{
    public class Company: Basic
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
        public DateTime FoundationDate { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Nacionality")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string Nacionality { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "HasStrangers")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string HasStrangers { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Currency")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public Guid CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public virtual Currency PaymentCurrency { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Address")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string Address { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Neighborhood")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string Neighborhood { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "City")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string City { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "State")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string State { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Country")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string Country { get; set; }
    }
}
