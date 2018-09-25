using System;
using System.Collections.Generic;
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
        public virtual JobRole JobRole { get; set; }

        public Guid WorkplaceId { get; set; }
        [ForeignKey("WorkplaceId")]
        public virtual Workplace Workplace { get; set; }

        public Guid FunctionId { get; set; }
        [ForeignKey("FunctionId")]
        public virtual Function Function { get; set; }

        public Guid ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public virtual Employee Manager { get; set; }

        public virtual IEnumerable<EmployeeHistory> Occurrences { get; set; }
        public virtual IEnumerable<Employee> Subordinates { get; set; }

        public string Nationality { get; set; }
        public string EmployeeNumber { get; set; }
        public DateTime DateBirth { get; set; }
        public string PersonalDocument { get; set; }
        public string PhoneNumber { get; set; }
    }
}
