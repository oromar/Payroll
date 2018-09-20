using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Company")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public Guid CompanyId { get; set; }

    }
}
