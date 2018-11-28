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
            var companies = _context
            .Company
            .Where(a => !a.IsDeleted)
            .ToList();

            ViewBag.Companies = companies;

            ViewBag.TotalEmployees = _context
            .Employee
            .Where(a => !a.IsDeleted)
            .Count();

             ViewBag.EmployeesByCompany = companies
            .Select(a => new
            {
                Key = a.Name,
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
            .Include(a => a.Company)
            .Include(a => a.Employees)            
            .Where(a => !a.IsDeleted)
            .Where(a => a.StartDate >= firstDayOfMonth)
            .Where(a => a.EndDate <= lastDayOfMonth)
            .ToDictionary(t => t.Company.Name, t => t.Employees);

            var today = DateTime.Today;

            ViewBag.EmployeesAbsents = _context
            .EmployeeHistory
            .Where(a => !a.IsDeleted)
            .Include(a => a.Employee)
            .Include(a => a.Employee.Company)
            .Include(a => a.OccurrenceType)
            .Where(a => a.OccurrenceType.IsAbsence)
            .Where(a => a.Start <= today)
            .Where(a => a.End >= today) 
            .ToDictionary(t => t.Employee, t => t.OccurrenceType);

            ViewBag.Projects = _context
            .Project
            .Include(a => a.Company)
            .Include(a => a.Employees)
            .Where(a => !a.IsDeleted)            
            .Where(a => a.Start <= today)
            .Where(a => a.End >= today)
            .ToDictionary(t => t, t => t.Employees); 

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
