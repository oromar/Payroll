using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            ViewBag.Companies = Utils
                .GetOptions(_businessObject
                .GetDAO()
                .GetContext()
                .Company
                .Where(a => !a.IsDeleted));

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

            return base.Index(page, filter, sort, order);
        }


        public override Task<IActionResult> Create([Bind] EmployeeHistory data)
        {
            var employee = _businessObject
                .GetDAO()
                .GetContext()
                .Employee
                .Find(data.EmployeeId);

            data.Name = employee.Name;

            ModelState.ClearValidationState(Constants.NAME_FIELD);
            ModelState.MarkFieldValid(Constants.NAME_FIELD);

            return base.Create(data);
        }
    }
}
