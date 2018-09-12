using System;
using System.ComponentModel.DataAnnotations;

namespace Payroll.Models
{
    public abstract class Basic
    {
        public Guid Id { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Name")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "RequiredField")]
        public string Name { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "CreatedAt")]
        public DateTime? CreatedAt { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "CreatedBy")]
        public string CreatedBy { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "UpdatedAt")]
        public DateTime? UpdatedAt { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "UpdatedBy")]
        public string UpdatedBy { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Deleted")]
        public Boolean IsDeleted { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "DeletedAt")]
        public DateTime? DeletedAt { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "DeletedBy")]
        public string DeletedBy { get; set; }
        public string SearchFields { get; set; }
    }
}
