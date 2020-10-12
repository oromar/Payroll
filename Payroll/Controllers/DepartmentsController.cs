using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payroll.Business;
using Payroll.Common;
using Payroll.Data;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class DepartmentsController : GenericController<Department>
    {
        public DepartmentsController(BusinessObject<Department> businessObject, Message message)
            : base(businessObject, message)
        {
        }

        public override Task<IActionResult> Index(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
            ViewBag.Companies = Utils
                .GetOptions(_businessObject
                .GetDAO()
                .Context
                .Company
                .Where(a => !a.IsDeleted));

            return base.Index(page, filter, sort, order);
        }

        public IActionResult DepartmentsByCompany(string companyId)
        {
            var departmens = Utils
                .GetOptions(_businessObject
                .GetDAO()
                .Context
                .Department
                .Include(a => a.Company)
                .Where(a => !a.IsDeleted)
                .Where(a => a.CompanyId.ToString() == companyId));

            return Ok(departmens);
        }
    }
}
