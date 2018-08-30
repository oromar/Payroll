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
    public class OccupationsController : Controller
    {
        private readonly OccupationsBO _businessObject;
        
        public OccupationsController(OccupationsBO occupationsBO)
        {
            _businessObject = occupationsBO;
        }

        // GET: Occupations
        public async Task<IActionResult> Index(int page = 1, string filter = "")
        {
            ViewData["CurrentPage"] = page;
            ViewData["CurrentFilter"] = filter;
            ViewData["HasMore"] = await _businessObject.HasMore(page, filter);

            return View(await _businessObject.Search(page, filter));
        }

        // GET: Occupations/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occupation = await _businessObject.Find((Guid)id);

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
                await _businessObject.Create(occupation, User.Identity.Name);                
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

            var occupation = await _businessObject.Find((Guid)id);
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
                    await _businessObject.Edit(id, occupation, User.Identity.Name);                    
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

            var occupation = await _businessObject.Find((Guid)id);                
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

            await _businessObject.Delete(id, User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }

        private bool OccupationExists(Guid id)
        {
            return _businessObject.OccupationExists(id);
        }
    }
}
