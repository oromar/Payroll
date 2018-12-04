using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Payroll.Models
{
    public class Certification 
    {
        public Guid CertificationId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "Name")]
        public string Name { get; set; }      

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "CertificationDate")]
        public DateTime Date { get; set; }

        public Guid EmployeeId {get; set;}

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

    }
}
