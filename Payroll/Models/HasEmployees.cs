using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public interface HasEmployees
    {
        IEnumerable<Employee> GetEmployees();
        void SetEmpoyees(IEnumerable<Employee> employees);
    }
}
