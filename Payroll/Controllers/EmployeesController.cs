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
            ViewBag.Companies = _businessObject
                .GetDAO()
                .GetContext()
                .Company
                .Where(a => !a.IsDeleted)
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                })
                .ToList();

            ViewBag.JobRoles = _businessObject
                .GetDAO()
                .GetContext()
                .JobRole
                .Where(a => !a.IsDeleted)
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                })
                .ToList();

            ViewBag.Workplaces = _businessObject
                .GetDAO()
                .GetContext()
                .Workplace
                .Where(a => !a.IsDeleted)
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                })
                .ToList();

            ViewBag.Occupations = _businessObject
                .GetDAO()
                .GetContext()
                .Occupation
                .Where(a => !a.IsDeleted)
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                })
                .ToList();

            ViewBag.Functions = _businessObject
                .GetDAO()
                .GetContext()
                .Function
                .Where(a => !a.IsDeleted)
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                })
                .ToList();

            ViewBag.Managers = _businessObject
                .GetDAO()
                .GetContext()
                .Employee
                .Include(a => a.Function)
                .Where(a => !a.IsDeleted)
                .Where(a => a.Function.IsManagerFunction)
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                })
                .ToList();

            return base.Index(page, filter, sort, order);
        }
    }
}
