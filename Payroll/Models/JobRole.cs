using System.ComponentModel.DataAnnotations;

namespace Payroll.Models
{
    public class JobRole: Basic
    {
        [Display(ResourceType = typeof(Resource), Name = "Description")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string Description { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Salary")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public double Salary { get; set; }
    }
}
