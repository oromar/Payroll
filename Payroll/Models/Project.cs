using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Payroll.Models
{
    public class Project: Basic
    {
        [Display(ResourceType =typeof(Resource), Name ="Name")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string Description { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "StartDate")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public DateTime Start { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "EndDate")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public DateTime End { get; set; }
        public virtual IEnumerable<ProjectEmployee> Employees { get; set; }
    }
}
