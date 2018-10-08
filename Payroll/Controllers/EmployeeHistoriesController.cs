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
        public EmployeeHistoriesController(EmployeeHistoryBO businessObject, Message message) :
            base(businessObject, message)
        {
        }
        public override Task<IActionResult> Index(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
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

    }
}
