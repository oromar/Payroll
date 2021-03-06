﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Business;
using Payroll.Common;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class ProjectsController : GenericController<Project>
    {
        public ProjectsController(BusinessObject<Project> businessObject, Message message)
            : base(businessObject, message) { }


        public override Task<IActionResult> Index(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
            var companies = _businessObject
                .GetDAO()
                .Context
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
                .Context
                .Employee
                .Where(b => !b.IsDeleted)
                .Where(c => c.CompanyId == a.Id)
                .ToList()
            })
            .ToDictionary(t => t.Key, t => t.Value);

            foreach (var i in dict.Keys)
            {
                foreach (var j in dict[i])
                {
                    j.Name = j.Name + " | " + @Resource.EmployeeNumber + ": " + j.EmployeeNumber;
                }
            }

            ViewBag.EmployeesByCompany = dict;

            var managers = companies.AsEnumerable()
            .Select(a => new
            {
                Key = a.Id,
                Value = _businessObject
                .GetDAO()
                .Context
                .Employee
                .Include(b => b.Function)
                .Where(b => !b.IsDeleted)
                .Where(b => b.Function.IsManagerFunction)
                .Where(c => c.CompanyId == a.Id)
                .ToList()
            })
            .ToDictionary(t => t.Key, t => t.Value);

            ViewBag.ManagersByCompany = managers;

            ViewBag.DepartmentsByCompany = companies.AsEnumerable()
            .Select(a => new
            {
                Key = a.Id,
                Value = _businessObject
                .GetDAO()
                .Context
                .Department
                .Where(b => !b.IsDeleted)
                .Where(c => c.CompanyId == a.Id)
                .ToList()
            })
            .ToDictionary(t => t.Key, t => t.Value);

            ViewBag.WorkplacesByCompany = companies.AsEnumerable()
            .Select(a => new
            {
                Key = a.Id,
                Value = _businessObject
                .GetDAO()
                .Context
                .Workplace
                .Where(b => !b.IsDeleted)
                .Where(c => c.CompanyId == a.Id)
                .ToList()
            })
            .ToDictionary(t => t.Key, t => t.Value);

            return base.Index(page, filter, sort, order);
        }

        public override Task<IActionResult> Edit(Guid id, [BindAttribute] Project data)
        {
            HandleEmployees(data);

            return base.Edit(id, data);
        }

        public override Task<IActionResult> Create([Bind] Project data)
        {
            HandleEmployees(data);

            return base.Create(data);
        }

        private void HandleEmployees(Project data)
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
        }
    }
}
