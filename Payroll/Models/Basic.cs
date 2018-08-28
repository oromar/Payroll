using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class Basic
    {
        public Guid Id { get; set; }
        [Display(Name = "Criado em")]
        public DateTime CreationTime { get; set; }
        [Display(Name = "Criado por")]
        public string CreationUser { get; set; }
        [Display(Name = "Alterado em")]
        public DateTime LastUpdateTime { get; set; }
        [Display(Name = "Alterado por")]
        public string LastUpdateUser { get; set; }
        [Display(Name = "Removido")]
        public Boolean Deleted { get; set; }
        [Display(Name = "Removido em")]
        public DateTime DeleteTime { get; set; }
        [Display(Name = "Removido por")]
        public string DeleteUser { get; set; }
    }
}
