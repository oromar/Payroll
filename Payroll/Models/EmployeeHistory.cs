using Payroll.Common;
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
            Expression<Func<EmployeeHistory, object>> result = null;

            switch (sort)
            {
                case Constants.SORT_OCCURRENCE_TYPE_NAME:
                    result = a => a.OccurrenceType.Name;
                    break;

                case Constants.SORT_OCCURRENCE:
                    result = a => a.Occurrence;
                    break;

                case Constants.SORT_END_DATE:
                    result = a => a.End;
                    break;

                case Constants.SORT_START_DATE:
                    result = a => a.Start;
                    break;

                case Constants.SORT_EMPLOYEE_NAME:
                    result = a => a.Employee.Name;
                    break;

                case Constants.SORT_CREATED_BY:
                    result = a => a.CreatedBy;
                    break;

                default:
                    result = a => a.Name;
                    break;
            }

            return result as Expression;
        }



    }
}
