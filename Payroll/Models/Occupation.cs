using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class Occupation : Basic
    {
        [Display(Name="Nome")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Name { get; set; }
        [Display(Name = "Possui Conselho?")]
        public bool IsRegulated { get; set; }
        [Display(Name = "Nome do Órgão de Classe")]
        public string CouncilName { get; set; }
    }
}
