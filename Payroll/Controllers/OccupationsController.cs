using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payroll.Common;
using Payroll.Data;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class OccupationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OccupationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Occupations
        public async Task<IActionResult> Index(int page = 1, string filter = "")
        {
            ViewData["CurrentPage"] = page;
            ViewData["CurrentFilter"] = filter;
            ViewData["HasMore"] = await _context.Occupation
                .Where(a => !a.Deleted)
                .Where(a => string.IsNullOrEmpty(filter) || (a.Name.Contains(filter) || a.CouncilName.Contains(filter)))
                .CountAsync() > (page * Constants.MAX_ITEMS_PER_PAGE);

            return View(await _context.Occupation
                                      .Where(a => !a.Deleted)
                                      .Where(a => string.IsNullOrEmpty(filter) || (a.Name.Contains(filter) || a.CouncilName.Contains(filter)))
                                      .OrderBy(a => a.Name)
                                      .Skip((page - 1) * Constants.MAX_ITEMS_PER_PAGE)
                                      .Take(Constants.MAX_ITEMS_PER_PAGE)
                                      .ToListAsync());
        }

        // GET: Occupations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occupation = await _context.Occupation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (occupation == null)
            {
                return NotFound();
            }

            return View(occupation);
        }

        // GET: Occupations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Occupations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsRegulated,CouncilName")] Occupation occupation)
        {
            if (ModelState.IsValid)
            {
                occupation.Id = Guid.NewGuid();
                occupation.CreationTime = DateTime.Now;
                occupation.Deleted = false;
                occupation.CreationUser = User.Identity.Name;
                _context.Add(occupation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(occupation);
        }

        // GET: Occupations/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occupation = await _context.Occupation.FindAsync(id);
            if (occupation == null)
            {
                return NotFound();
            }
            return View(occupation);
        }

        // POST: Occupations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,IsRegulated,CouncilName,Id,CreationTime,CreationUser")] Occupation occupation)
        {
            if (id != occupation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    occupation.LastUpdateTime = DateTime.Now;
                    occupation.LastUpdateUser = User.Identity.Name;
                    _context.Update(occupation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OccupationExists(occupation.Id))
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
            return View(occupation);
        }

        // GET: Occupations/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occupation = await _context.Occupation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (occupation == null)
            {
                return NotFound();
            }

            return View(occupation);
        }

        // POST: Occupations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var occupation = await _context.Occupation.FindAsync(id);
            occupation.Deleted = true;
            occupation.DeleteUser = User.Identity.Name;
            _context.Occupation.Update(occupation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OccupationExists(Guid id)
        {
            return _context.Occupation.Any(e => e.Id == id);
        }
    }
}
