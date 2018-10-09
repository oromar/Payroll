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
    public class ProjectsController : GenericController<Project>
    {
        public ProjectsController(ProjectBO businessObject, Message message) 
            : base(businessObject, message) { }


        public override Task<IActionResult> Index(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
            ViewBag.Companies = Utils
                .GetOptions(_businessObject
                .GetDAO()
                .GetContext()
                .Company
                .Where(a => !a.IsDeleted));

            return base.Index(page, filter, sort, order);
        }

        public override Task<IActionResult> Create([Bind] Project data)
        {
            var projectEmployees = HttpContext
                .Request
                .Form[Constants.EMPLOYEE_ID]
                .Select(a => new ProjectEmployee
                {
                    EmployeeId = Guid.Parse(a)
                })
                .ToList();

            data.Employees = projectEmployees;
           
            return base.Create(data);
        }
    }
}
