using System;

namespace Payroll.Models
{
    public class WorkHoursEmployee
    {
        public Guid WorkHoursId { get; set; }
        public WorkHours WorkHours { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
