using Payroll.Data;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Business
{
    public class EmployeeHistoryBO : BusinessObject<EmployeeHistory>
    {
        public EmployeeHistoryBO(GenericDAO<EmployeeHistory> dao) : base(dao)
        {

        }

        public override async Task<EmployeeHistory> Create(EmployeeHistory data, string userIdentity)
        {
            var employee = _dao
                .GetContext()
                .Employee
                .Find(data.EmployeeId);

            if (employee != null)
            {
                data.Name = employee.Name;
            }
            else
            {
                data.Name = string.Empty;
            }

            return await base.Create(data, userIdentity);
        }
    }
}
