using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Payroll.Business;
using Payroll.Common;
using Payroll.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Controllers
{
    public class WorkplacesController : GenericController<Workplace>
    {
        public WorkplacesController(BusinessObject<Workplace> businessObject, Message message) : 
            base(businessObject, message) {}

        public override async Task<IActionResult> Index(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
            ViewBag.Companies = Utils
                .GetOptions(_businessObject
                .GetDAO()
                .GetContext()
                .Company
                .Where(a => !a.IsDeleted));

            return await base.Index(page, filter, sort, order);
        }
    }
}
