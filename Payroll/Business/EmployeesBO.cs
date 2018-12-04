using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Models;

namespace Payroll.Business
{
    public class EmployeesBO : BusinessObject<Employee>
    {
        public EmployeesBO(GenericDAO<Employee> dao) : base(dao)
        {
        }

        public override Task<Employee> Edit(Guid id, Employee data, string userIdentity)
        {
            _dao
                .GetContext()
                .Certification
                .RemoveRange(_dao
                    .GetContext()
                    .Certification
                    .Where(a => a.EmployeeId == id));

            return base.Edit(id, data, userIdentity);
        }

        public override async Task<List<Employee>> Search(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
            var result = await base.Search(page, filter, sort, order);

            result.ForEach(a =>
            {
                a.Certifications = _dao
                .GetContext()
                .Employee
                .Include(b => b.Certifications)
                .First(b => b.Id == a.Id)
                .Certifications
                .DefaultIfEmpty()
                .ToList();
            });

            return result;
        }
    }
}