using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Payroll.Business;
using Payroll.Common;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class WorkHoursController : GenericController<WorkHours>
    {
        public WorkHoursController(WorkHoursBO whBO, Message message) :
            base(whBO, message)
        { }

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

            ViewBag.DaysOfWeek = Utils.GetDaysOfWeek();

            return base.Index(page, filter, sort, order);
        }

        public override Task<IActionResult> Edit(Guid id, [BindAttribute] WorkHours data)
        {
            HandleItems(data);

            HandleEmployees(data);

            return base.Edit(id, data);
        }

        public override Task<IActionResult> Create([Bind] WorkHours data)
        {
            HandleItems(data);

            HandleEmployees(data);

            return base.Create(data);
        }

        public IActionResult DaysOfWeek()
        {
            return Ok(Utils.GetDaysOfWeek());
        }

        private void HandleEmployees(WorkHours data)
        {
            var employees = HttpContext
                            .Request
                            .Form[Constants.EMPLOYEE_ID]
                            .Select(a => new WorkHoursEmployee
                            {
                                EmployeeId = Guid.Parse(a)
                            })
                            .ToList();

            data.Employees = employees;
        }

        private void HandleItems(WorkHours data)
        {
            var itemsToAdd = new List<WorkHourItem>();

            var daysOfWeek = HttpContext
                .Request
                .Form[Constants.DAY_OF_WEEK];

            for (int i = 0; i < daysOfWeek.Count; i++)
            {
                var start = HttpContext
                    .Request
                    .Form[Constants.START][i];

                var end = HttpContext
                    .Request
                    .Form[Constants.END][i];

                itemsToAdd.Add(new WorkHourItem { DayOfWeek = (Common.DayOfWeek)Enum.Parse(typeof(Common.DayOfWeek), daysOfWeek[i]), Start = TimeSpan.Parse(start), End = TimeSpan.Parse(end) });
            }

            data.WorkHourItems = itemsToAdd;
        }

    }
}
