using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Business;
using Payroll.Common;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class EmployeeHistoriesController : GenericController<EmployeeHistory>
    {
        public EmployeeHistoriesController(BusinessObject<EmployeeHistory> businessObject, Message message) :
            base(businessObject, message)
        {
        }
        public override Task<IActionResult> Index(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
            var companies = _businessObject
                .GetDAO()
                .GetContext()
                .Company
                .Where(a => !a.IsDeleted);

            ViewBag.Companies = Utils
                .GetOptions(companies);

            ViewBag.Employees = Utils
                .GetOptions(_businessObject
                .GetDAO()
                .GetContext()
                .Employee                
                .Where(a => !a.IsDeleted));

            ViewBag.OccurrenceTypes = Utils
                .GetOptions(_businessObject
                .GetDAO()
                .GetContext()
                .OccurrenceType
                .Where(a => !a.IsDeleted));

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
            
            var departments = companies
            .Include(a => a.Departments)
            .Where(a => !a.IsDeleted)
            .SelectMany(a => a.Departments);

            ViewBag.EmployeesByDepartments = departments
            .AsEnumerable()
            .Select (a => new 
            {
                Key = a.Id,
                Value = _businessObject
                .GetDAO()
                .GetContext()
                .Employee
                .Where(b => !b.IsDeleted)
                .Where(b => b.DepartmentId == a.Id)
                .ToList()

            })
            .ToDictionary(t => t.Key, t => t.Value);

            return  base.Index(page, filter, sort, order);
        }


        public override Task<IActionResult> Create([Bind] EmployeeHistory data)
        {
            HandleName(data);

            return base.Create(data);
        }

        public override Task<IActionResult> Edit(System.Guid id, EmployeeHistory data)
        {
            HandleName(data);

            return base.Edit(id, data);
        }

        private void HandleName(EmployeeHistory data) {

            var employee = _businessObject
                .GetDAO()
                .GetContext()
                .Employee
                .Find(data.EmployeeId);

            data.Name = employee.Name;

            ModelState.ClearValidationState(Constants.NAME_FIELD);
            ModelState.MarkFieldValid(Constants.NAME_FIELD);
        }
    }
}
