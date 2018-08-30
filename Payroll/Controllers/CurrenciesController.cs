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
        private readonly CurrenciesBO _currenciesBO;
        private readonly Message _message;

        public CurrenciesController(CurrenciesBO currenciesBO, Message message)
        {
            _currenciesBO = currenciesBO;
            _message = message;
        }

        public async Task<IActionResult> Index(int page = 1, string filter = "")
        {
            ViewData["CurrentPage"] = page;
            ViewData["CurrentFilter"] = filter;
            ViewData["HasMore"] = await _currenciesBO.HasMore();

            if (_message.HasMessage)
            {
                ViewBag.MessageTitle = _message.Title;
                ViewBag.Message = _message.Body;
                ViewBag.MessageType = _message.Type;

                _message.Clear();
            }

            return View(await _currenciesBO.Search(page, filter));
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _currenciesBO.Find(id);

            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Exchange,Symbol")] Currency currency)
        {
            if (ModelState.IsValid)
            {
                await _currenciesBO.Create(currency, User.Identity.Name);

                CreateMessage(Resource.SuccessMessageType, Resource.CreatedSuccessfully_a);

                return RedirectToAction(nameof(Index));
            }
            return View(currency);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _currenciesBO.Find(id);
            if (currency == null)
            {
                return NotFound();
            }
            return View(currency);
        }

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
                    await _currenciesBO.Edit(id, currency, User.Identity.Name);

                    CreateMessage(Resource.SuccessMessageType, Resource.UpdatedSuccessfully_a);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!CurrencyExists(currency.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        CreateMessage(Resource.DangerMessageType, e.Message);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(currency);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _currenciesBO.Find(id);

            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _currenciesBO.Delete(id, User.Identity.Name);

            CreateMessage(Resource.SuccessMessageType, Resource.RemovedSuccessfully_a);

            return RedirectToAction(nameof(Index));
        }

        private bool CurrencyExists(Guid id)
        {
            return _currenciesBO.Exists(id);
        }

        private void CreateMessage(string type, string message)
        {
            _message.Title = Resource.Currency;
            _message.Body = message;            
            _message.Type = type;
        }
    }
}
