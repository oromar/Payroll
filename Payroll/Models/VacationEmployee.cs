using System;

namespace Payroll.Models
{
    public class VacationEmployee
    {
        public Guid VacationId { get; set; }
        public Vacation Vacation { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
