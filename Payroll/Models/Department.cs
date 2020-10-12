using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Models
{
    public class Department : Basic
    {
        [Display(ResourceType = typeof(Resource), Name = "Company")]
        public Guid CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "Description")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string Description { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "IsManagerDepartment")]
        public bool IsManagerDepartment { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "IsOperationalDepartment")]
        public bool IsOperationalDepartment { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "IsDirectionDepartment")]
        public bool IsDirectionDepartment { get; set; }

        public virtual IEnumerable<Employee> Employees { get; set; }
    }
}
