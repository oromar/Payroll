using System.ComponentModel.DataAnnotations;

namespace Payroll.Models
{
    public class Currency: Basic
    {
        [Display(ResourceType = typeof(Resource), Name = "Exchange")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName ="RequiredField")]
        public double Exchange { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "Symbol")]
        public string Symbol { get; set; }
    }
}
