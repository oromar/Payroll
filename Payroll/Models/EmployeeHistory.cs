using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Models
{
    public class EmployeeHistory: Basic
    {
        [Display(ResourceType = typeof(Resource), Name = "EmployeeName")]
        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        [Display(ResourceType = typeof(Resource), Name = "OccurrenceType")]
        public Guid OccurrenceTypeId { get; set; }
        [ForeignKey("OccurrenceTypeId")]       
        public virtual OccurrenceType OccurrenceType { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "Occurrence")]
        public string Occurrence { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "StartDate")]
        public DateTime? Start { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        [Display(ResourceType = typeof(Resource), Name = "EndDate")]
        public DateTime? End { get; set; }
    }
}
