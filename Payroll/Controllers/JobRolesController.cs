using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Payroll.Business;
using Payroll.Common;
using Payroll.Controllers;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class JobRolesController : GenericController<JobRole>
    {
        public JobRolesController(BusinessObject<JobRole> businessObject, Message message)
            : base(businessObject, message) { }

        public override async Task<IActionResult> Index(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
            ViewBag.Companies = _businessObject
                .GetContext()
                .Company
                .Where(a => !a.IsDeleted)
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                })
                .ToList();

            return await base.Index(page, filter, sort, order);
        }

    }
}
