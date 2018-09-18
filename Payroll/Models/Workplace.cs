using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Models
{
    public class Workplace: Basic
    {
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Company")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public Guid CompanyId { get; set; }
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
