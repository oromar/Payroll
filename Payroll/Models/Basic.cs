using System;
using System.ComponentModel.DataAnnotations;

namespace Payroll.Models
{
    public abstract class Basic
    {
        public Guid Id { get; set; }
        [Display(Name = "Criado em")]
        public DateTime? CreatedAt { get; set; }
        [Display(Name = "Criado por")]
        public string CreatedBy { get; set; }
        [Display(Name = "Alterado em")]
        public DateTime? UpdatedAt { get; set; }
        [Display(Name = "Alterado por")]
        public string UpdatedBy { get; set; }
        [Display(Name = "Removido")]
        public Boolean IsDeleted { get; set; }
        [Display(Name = "Removido em")]
        public DateTime? DeletedAt { get; set; }
        [Display(Name = "Removido por")]
        public string DeletedBy { get; set; }
        public string SearchFields { get; set; }
    }
}
