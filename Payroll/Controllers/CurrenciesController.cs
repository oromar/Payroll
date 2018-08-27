using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payroll.Data;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class CurrenciesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CurrenciesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Currencies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Currency.Where(a => !a.Deleted).ToListAsync());
        }

        // GET: Currencies/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currency
                .Where(a => !a.Deleted)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // GET: Currencies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Currencies/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Exchange")] Currency currency)
        {
            if (ModelState.IsValid)
            {
                currency.Id = Guid.NewGuid();
                currency.CreationTime = DateTime.Now;
                currency.Deleted = false;
                currency.CreationUser = User.Identity.Name;
                _context.Add(currency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(currency);
        }

        // GET: Currencies/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currency.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }
            return View(currency);
        }

        // POST: Currencies/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Exchange,Id,CreationTime,CreationUser")] Currency currency)
        {
            if (id != currency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    currency.LastUpdateTime = DateTime.Now;
                    currency.LastUpdateUser = User.Identity.Name;
                    _context.Update(currency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrencyExists(currency.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(currency);
        }

        // GET: Currencies/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currency
                .Where(a => !a.Deleted)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // POST: Currencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var currency = await _context.Currency.FindAsync(id);
            currency.Deleted = true;
            currency.DeleteTime = DateTime.Now;
            currency.DeleteUser = User.Identity.Name;
            _context.Currency.Update(currency);            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CurrencyExists(Guid id)
        {
            return _context.Currency.Any(e => e.Id == id);
        }
    }
}
