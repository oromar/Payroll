using System.ComponentModel.DataAnnotations;

namespace Payroll.Models
{
    public class LicenseType : Basic
    {
        [Required(ErrorMessage="Campo obrigatório")]
        [Display(Name="Descrição")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Quantidade de dias padrão")]
        public int QtyDaysDefault { get; set; }
    }
}
