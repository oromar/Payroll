using MMLib.Extensions;
using Payroll.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Payroll.Models
{
    public class OccurrenceType: Basic
    {
        [Display(ResourceType = typeof(Resource), Name = "Description")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string Description { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "IsAbsence")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public bool IsAbsence { get; set; }
    }
}
