using System.ComponentModel.DataAnnotations;

namespace Payroll.Models
{
    public class Occupation : Basic
    {
        [Display(Name = "Possui Conselho?")]
        public bool IsRegulated { get; set; }
        [Display(Name = "Nome do Órgão de Classe")]
        public string CouncilName { get; set; }
    }
}
