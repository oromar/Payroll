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
    public class CurrenciesController : Controller
    {
        private readonly CurrenciesBO _bussinessObject;

        public CurrenciesController(CurrenciesBO currenciesBO)
        {
            _bussinessObject = currenciesBO;
        }

        // GET: Currencies
        public async Task<IActionResult> Index(int page = 1, string filter = "")
        {

            ViewData["CurrentPage"] = page;
            ViewData["CurrentFilter"] = filter;
            ViewData["HasMore"] = await _bussinessObject.HasMore();

            return View(await _bussinessObject.Search(page, filter));
        }

        // GET: Currencies/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _bussinessObject.Find(id);

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
        public async Task<IActionResult> Create([Bind("Name,Exchange,Symbol")] Currency currency)
        {
            if (ModelState.IsValid)
            {
                await _bussinessObject.Create(currency, User.Identity.Name);
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

            var currency = await _bussinessObject.Find(id);
            if (currency == null)
            {
                return NotFound();
            }
            return View(currency);
        }

        // POST: Currencies/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Exchange,Symbol,Id,CreationTime,CreationUser")] Currency currency)
        {
            if (id != currency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bussinessObject.Edit(id, currency, User.Identity.Name);
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

            var currency = await _bussinessObject.Find(id);                
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
            await _bussinessObject.Delete(id, User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }

        private bool CurrencyExists(Guid id)
        {
            return _bussinessObject.CurrencyExists(id);
        }
    }
}
