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
    public class VacationsController : GenericController<Vacation>
    {
        public VacationsController(VacationBO businessObject, Message message) : base(businessObject, message) { }


        public override Task<IActionResult> Index(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
            var companies = _businessObject
                .GetDAO()
                .GetContext()
                .Company
                .Where(a => !a.IsDeleted);

            ViewBag.Companies = Utils
                .GetOptions(companies);


            var dict = companies.AsEnumerable()
            .Select(a => new
            {
                Key = a.Id,
                Value = _businessObject
                .GetDAO()
                .GetContext()
                .Employee
                .Where(b => !b.IsDeleted)
                .Where(c => c.CompanyId == a.Id)
                .ToList()                
            })
            .ToDictionary(t => t.Key, t => t.Value);

            foreach(var i in dict.Keys)
            {
                foreach(var j in dict[i])
                {
                    j.Name = j.Name + " | " + @Resource.EmployeeNumber + ": " + j.EmployeeNumber;
                }
            }

            ViewBag.EmployeesByCompany = dict;

            return base.Index(page, filter, sort, order);
        }

        public override Task<IActionResult> Edit(Guid id, [BindAttribute] Vacation data)
        {
            HandleEmployees(data);

            return base.Edit(id, data);
        }


        public override Task<IActionResult> Create([Bind] Vacation data)
        {
            HandleEmployees(data);

            return base.Create(data);
        }

        private void HandleEmployees(Vacation data)
        {
            var vacationsEmployees = HttpContext
                            .Request
                            .Form[Constants.EMPLOYEE_ID]
                            .Select(a => new VacationEmployee
                            {
                                EmployeeId = Guid.Parse(a)
                            })
                            .ToList();

            data.Employees = vacationsEmployees;
        }
    }
}
