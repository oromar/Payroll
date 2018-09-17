using Microsoft.AspNetCore.Mvc;
using Payroll.Business;
using Payroll.Common;
using Payroll.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Payroll.Controllers
{
    public class CompaniesController : GenericController<Company>
    {
        public CompaniesController(BusinessObject<Company> companyBO, Message message) : 
            base (companyBO, message) {}

        public override async Task<IActionResult> Index(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
            ViewBag.Currencies = _businessObject
                .GetContext()
                .Currency
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
