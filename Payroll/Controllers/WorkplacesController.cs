﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            base(businessObject, message)
        { }

        public override async Task<IActionResult> Index(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
            ViewBag.Companies = Utils
                .GetOptions(_businessObject
                .GetDAO()
                .Context
                .Company
                .Where(a => !a.IsDeleted));

            return await base.Index(page, filter, sort, order);
        }


        public IActionResult WorkplacesByCompany(string companyId)
        {
            var workplaces = Utils
                .GetOptions(_businessObject
                .GetDAO()
                .Context
                .Workplace
                .Include(a => a.Company)
                .Where(a => !a.IsDeleted)
                .Where(a => a.CompanyId.ToString() == companyId));

            return Ok(workplaces);
        }
    }
}
