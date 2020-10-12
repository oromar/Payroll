using Microsoft.AspNetCore.Mvc;
using Payroll.Business;
using Payroll.Common;
using Payroll.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Payroll.Controllers
{
    public class CompaniesController : GenericController<Company>
    {
        public CompaniesController(BusinessObject<Company> companyBO, Message message) :
            base(companyBO, message)
        { }

        public override async Task<IActionResult> Index(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
            ViewBag.Currencies = Utils
                .GetOptions(_businessObject
                .GetDAO()
                .Context
                .Currency
                .Where(a => !a.IsDeleted));

            return await base.Index(page, filter, sort, order);
        }

    }
}
