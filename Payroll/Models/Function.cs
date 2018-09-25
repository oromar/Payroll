using System.ComponentModel.DataAnnotations;

namespace Payroll.Models
{
    public class Function: Basic
    {
        [Display(ResourceType = typeof(Resource), Name = "Description")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string Description { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "IsManagerFunction")]
        public bool IsManagerFunction { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "ManagerCommission")]
        public double ManagerCommission { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "HasDangeroues")]
        public bool HasDangerous { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "HasUnhealthy")]
        public bool HasUnhealthy { get; set; }
    }
}
