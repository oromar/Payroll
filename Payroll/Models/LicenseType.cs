using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Payroll.Models
{
    public class LicenseType : Basic
    {
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName ="RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "Description")]
        public string Description { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "QtyDaysDefault")]
        public int QtyDaysDefault { get; set; }
    }
}
