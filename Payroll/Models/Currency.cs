using System.ComponentModel.DataAnnotations;

namespace Payroll.Models
{
    public class Currency: Basic
    {
        [Display(Name = "Câmbio")]
        [Required(ErrorMessage ="Campo obrigatório")]
        public double Exchange { get; set; }
        [Display(Name = "Símbolo")]
        public string Symbol { get; set; }
    }
}
