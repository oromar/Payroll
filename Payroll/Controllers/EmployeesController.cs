using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payroll.Business;
using Payroll.Common;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class EmployeesController : GenericController<Employee>
    {
        public EmployeesController(EmployeesBO businessObject, Message message)
            : base(businessObject, message) { }

        public override Task<IActionResult> Index(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
            var companies = _businessObject
                .GetDAO()
                .GetContext()
                .Company
                .Where(a => !a.IsDeleted);

            ViewBag.Companies = Utils
                .GetOptions(companies);

            ViewBag.Occupations = Utils
                .GetOptions(_businessObject
                .GetDAO()
                .GetContext()
                .Occupation
                .Where(a => !a.IsDeleted));

            ViewBag.Genders = Utils.GetGenders();

            ViewBag.DepartmentsByCompany = companies.AsEnumerable()
            .Select(a => new
            {
                Key = a.Id,
                Value = _businessObject
                .GetDAO()
                .GetContext()
                .Department
                .Where(b => !b.IsDeleted)
                .Where(c => c.CompanyId == a.Id)
                .ToList()
            })
            .ToDictionary(t => t.Key, t => t.Value);

            ViewBag.WorkplacesByCompany = companies.AsEnumerable()
            .Select(a => new
            {
                Key = a.Id,
                Value = _businessObject
                .GetDAO()
                .GetContext()
                .Workplace
                .Where(b => !b.IsDeleted)
                .Where(c => c.CompanyId == a.Id)
                .ToList()
            })
            .ToDictionary(t => t.Key, t => t.Value);


            ViewBag.JobRolesByCompany = companies.AsEnumerable()
            .Select(a => new
            {
                Key = a.Id,
                Value = _businessObject
                .GetDAO()
                .GetContext()
                .JobRole
                .Where(b => !b.IsDeleted)
                .Where(c => c.CompanyId == a.Id)
                .ToList()
            })
            .ToDictionary(t => t.Key, t => t.Value);

            ViewBag.FunctionsByCompany = companies.AsEnumerable()
            .Select(a => new
            {
                Key = a.Id,
                Value = _businessObject
                .GetDAO()
                .GetContext()
                .Function
                .Where(b => !b.IsDeleted)
                .Where(c => c.CompanyId == a.Id)
                .ToList()
            })
            .ToDictionary(t => t.Key, t => t.Value);

            ViewBag.ManagersByCompany = companies.AsEnumerable()
            .Select(a => new
            {
                Key = a.Id,
                Value = _businessObject
                .GetDAO()
                .GetContext()
                .Employee
                .Include(b => b.Function)
                .Where(b => !b.IsDeleted)
                .Where(c => c.CompanyId == a.Id)
                .Where(b => b.Function.IsManagerFunction)
                .ToList()
            })
            .ToDictionary(t => t.Key, t => t.Value);

            return base.Index(page, filter, sort, order);
        }

        public override async Task<IActionResult> Create(Employee data)
        {
            HandleItems(data);

            return await base.Create(data);
        }

        public override async Task<IActionResult> Edit(Guid id, Employee data)
        {
            HandleItems(data);

            return await base.Edit(id, data);
        }

        public IActionResult EmployeesByDepartment(string departmentId)
        {
            var employeesFromDB = _businessObject
                .GetDAO()
                .GetContext()
                .Employee
                .Include(a => a.Department)
                .Where(a => !a.IsDeleted)
                .Where(a => a.DepartmentId.ToString() == departmentId)
                .ToList();

            employeesFromDB.ForEach(Utils.GetEmployeeOption);


            var employees = Utils
                .GetOptions(employeesFromDB);

            return Ok(employees);
        }

        public IActionResult EmployeesByCompany(string companyId)
        {
            var employeesFromDB = _businessObject
                .GetDAO()
                .GetContext()
                .Employee
                .Include(a => a.Company)
                .Where(a => !a.IsDeleted)
                .Where(a => a.CompanyId.ToString() == companyId)
                .ToList();

            employeesFromDB
            .ForEach(Utils.GetEmployeeOption);

            var employees = Utils
                .GetOptions(employeesFromDB);

            return Ok(employees);
        }

        public IActionResult ManagersByCompany(string companyId)
        {
            var employeesFromDB = _businessObject
                .GetDAO()
                .GetContext()
                .Employee
                .Include(a => a.Function)
                .Include(a => a.Company)
                .Where(a => !a.IsDeleted)
                .Where(a => a.CompanyId.ToString() == companyId)
                .Where(a => a.Function.IsManagerFunction)
                .ToList();

            employeesFromDB
            .ForEach(Utils.GetEmployeeOption);


            var employees = Utils
                .GetOptions(employeesFromDB);

            return Ok(employees);
        }

        private void HandleItems(Employee data)
        {
            var itemsToAdd = new List<Certification>();

            var names = HttpContext
                .Request
                .Form[Constants.CERTIFICATION_NAME];
            
            var dates = HttpContext
                .Request
                .Form[Constants.CERTIFICATION_DATE];

            for (int i = 0; i < names.Count; i++)
            {
                itemsToAdd.Add(new Certification 
                {
                     Name = names[i], 
                     Date = DateTime.Parse(dates[i])
                     
                });
            }
            data.Certifications = itemsToAdd;
        }
    }
}
