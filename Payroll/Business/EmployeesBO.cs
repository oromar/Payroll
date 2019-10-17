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
        public EmployeesBO(GenericDAO<Employee> dao, CreateEmployeeBusinessRule createRule, 
        DeleteEmployeeBusinessRule deleteRule, EditEmployeeBusinessRule editRule) : 
        base(dao, createRule, editRule, deleteRule)
        {
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