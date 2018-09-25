using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll.Models
{
    public class Employee : Addressable
    {
        public Guid CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        public Guid JobRoleId { get; set; }
        [ForeignKey("JobRoleId")]
        public JobRole JobRole { get; set; }

        public Guid WorkplaceId { get; set; }
        [ForeignKey("WorkplaceId")]
        public Workplace Workplace { get; set; }

        public Guid FunctionId { get; set; }
        [ForeignKey("FunctionId")]
        public Function Function { get; set; }

        public string Nationality { get; set; }
        public string EmployeeNumber { get; set; }
        public DateTime DateBirth { get; set; }
        public string PersonalDocument { get; set; }
        public string PhoneNumber { get; set; }
    }
}
