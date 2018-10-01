using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace Payroll.Models
{
    public class EmployeeHistory: Basic
    {
        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        public Guid OccurrenceTypeId { get; set; }
        [ForeignKey("OccurrenceTypeId")]
        public OccurrenceType OccurrenceType { get; set; }

        public string Occurrence { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public override Expression SortBy(string sort)
        {
            throw new NotImplementedException();
        }
    }
}
