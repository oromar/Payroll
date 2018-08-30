using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class Currency: Basic
    {
        [Display(Name="Moeda")]
        [Required(ErrorMessage ="Campo obrigatório")]
        public String Name { get; set; }
        [Display(Name = "Câmbio")]
        [Required(ErrorMessage ="Campo obrigatório")]
        public double Exchange { get; set; }
        [Display(Name = "Símbolo")]
        public string Symbol { get; set; }

    }
}
