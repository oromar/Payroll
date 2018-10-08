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
        public EmployeesController(BusinessObject<Employee> businessObject, Message message)
            : base(businessObject, message) {}

        public override Task<IActionResult> Index(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
            ViewBag.Companies = Utils
                .GetOptions(_businessObject
                .GetDAO()
                .GetContext()
                .Company
                .Where(a => !a.IsDeleted));

            ViewBag.JobRoles = Utils
                .GetOptions(_businessObject
                .GetDAO()
                .GetContext()
                .JobRole
                .Where(a => !a.IsDeleted));

            ViewBag.Workplaces = Utils
                .GetOptions(_businessObject
                .GetDAO()
                .GetContext()
                .Workplace
                .Where(a => !a.IsDeleted));

            ViewBag.Occupations = Utils
                .GetOptions(_businessObject
                .GetDAO()
                .GetContext()
                .Occupation
                .Where(a => !a.IsDeleted));

            ViewBag.Functions = Utils
                .GetOptions(_businessObject
                .GetDAO()
                .GetContext()
                .Function
                .Where(a => !a.IsDeleted));

            ViewBag.Managers = Utils
                .GetOptions(_businessObject
                .GetDAO()
                .GetContext()
                .Employee
                .Include(a => a.Function)
                .Where(a => !a.IsDeleted)
                .Where(a => a.Function.IsManagerFunction));

            ViewBag.Genders = Utils.GetGenders();

            return base.Index(page, filter, sort, order);
        }


        public IActionResult EmployeesByDepartment(string departmentId)
        {
            var employees = _businessObject
                .GetDAO()
                .GetContext()
                .Employee
                .Include(a => a.Department)
                .Where(a => !a.IsDeleted)
                .Where(a => a.DepartmentId.ToString() == departmentId)
                .Select(a => new SelectListItem {
                        Text = a.Name,
                        Value = a.Id.ToString()
                })
                .ToList();

            return Ok(employees);
        }
    }
}
