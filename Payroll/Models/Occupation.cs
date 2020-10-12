using System.ComponentModel.DataAnnotations;

namespace Payroll.Models
{
    public class Occupation : Basic
    {
        [Display(ResourceType = typeof(Resource), Name = "HasCouncil")]
        public bool IsRegulated { get; set; }
        [Display(ResourceType = typeof(Resource), Name = "CouncilName")]
        public string CouncilName { get; set; }
    }
}
