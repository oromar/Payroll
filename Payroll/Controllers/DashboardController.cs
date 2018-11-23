using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Common;
using Payroll.Data;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.TotalEmployees = _context
            .Employee
            .Where(a => !a.IsDeleted)
            .Count();

             ViewBag.EmployeesByCompany = _context
             .Company
             .Where(a => !a.IsDeleted)
             .AsEnumerable()
            .Select(a => new
            {
                Key = a.Id,
                Value = _context
                .Employee
                .Where(b => !b.IsDeleted)
                .Where(c => c.CompanyId == a.Id)
                .ToList()
            })
            .ToDictionary(t => t.Key, t => t.Value);
            
            var firstDayOfMonth = DateTime.Now.GetFirstDayOfMonth();
            var lastDayOfMonth = DateTime.Now.GetLastDayOfMonth();

            ViewBag.EmployeesInVacations = _context
            .Vacation
            .Where(a => !a.IsDeleted)
            .Include(a => a.Employees)
            .Where(a => a.StartDate >= firstDayOfMonth)
            .Where(a => a.EndDate <= lastDayOfMonth)
            .SelectMany(a => a.Employees)
            .ToList();

            ViewBag.EmployeesAbsents = _context
            .EmployeeHistory
            .Where(a => !a.IsDeleted)
            .Include(a => a.Employee)
            .Include(a => a.OccurrenceType)
            .Where(a => a.OccurrenceType.IsAbsence)
            .Where(a => a.Start <= DateTime.Today)
            .Where(a => a.End >= DateTime.Today) 
            .Select(a => a.Employee)
            .ToList();

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
