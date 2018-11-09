using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            ViewBag.Companies = Utils
                .GetOptions(_businessObject
                .GetDAO()
                .GetContext()
                .Company
                .Where(a => !a.IsDeleted));

            return await base.Index(page, filter, sort, order);
        }

        public IActionResult JobRolesByCompany(string companyId)
        {
            var jobRoles = Utils
                .GetOptions(_businessObject
                .GetDAO()
                .GetContext()
                .JobRole
                .Include(a => a.Company)
                .Where(a => !a.IsDeleted)
                .Where(a => a.CompanyId.ToString() == companyId));

            return Ok(jobRoles);
        }

    }
}
