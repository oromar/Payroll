using System.ComponentModel.DataAnnotations;

namespace Payroll.Models
{
    public abstract class Addressable : Basic
    {
        [Display(ResourceType = typeof(Resource), Name = "Address")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string Address { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Number")]
        public string Number { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Neighborhood")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string Neighborhood { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Complement")]
        public string Complement { get; set; }  
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
