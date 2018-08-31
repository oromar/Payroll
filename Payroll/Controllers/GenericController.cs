using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Business;
using Payroll.Common;
using Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Controllers
{
    public abstract class GenericController<T> : Controller where T : Basic
    {
        protected readonly GenericBO<T> _businessObject;

        protected readonly Message _message;

        protected readonly string _entityName = string.Empty;

        public GenericController(GenericBO<T> genericBO, Message message, string entityName)
        {
            _businessObject = genericBO;
            _message = message;
            _entityName = entityName;
        }

        public void CreateMessage(string type, string message)
        {
            _message.Title = _entityName;
            _message.Body = message;
            _message.Type = type;
        }

        public async Task<IActionResult> Index(int page = 1, string filter = "")
        {
            var totalItems = await _businessObject.Count(page, filter);
            ViewBag.CurrentPage = page;
            ViewBag.CurrentFilter = filter;
            ViewBag.TotalItems = totalItems;
            ViewBag.HasMore = totalItems > (page * Constants.MAX_ITEMS_PER_PAGE);
            ViewBag.ItemsPerPage = Constants.MAX_ITEMS_PER_PAGE;

            if (_message.HasMessage)
            {
                ViewBag.MessageTitle = _message.Title;
                ViewBag.Message = _message.Body;
                ViewBag.MessageType = _message.Type;

                _message.Clear();
            }

            return View(await _businessObject.Search(page, filter));
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _businessObject.Find(id);

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
        public async Task<IActionResult> Create([Bind] T data)
        {
            if (ModelState.IsValid)
            {
                await _businessObject.Create(data, User.Identity.Name);

                CreateMessage(Resource.SuccessMessageType, Resource.CreatedSuccessfully_a);

                return RedirectToAction(nameof(Index));
            }
            return View(data);
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _businessObject.Find(id);
            if (currency == null)
            {
                return NotFound();
            }
            return View(currency);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind] T data)
        {
            if (id != data.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _businessObject.Edit(id, data, User.Identity.Name);

                    CreateMessage(Resource.SuccessMessageType, Resource.UpdatedSuccessfully_a);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    if (!Exists(data.Id))
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
            return View(data);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _businessObject.Find(id);

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
            await _businessObject.Delete(id, User.Identity.Name);

            CreateMessage(Resource.SuccessMessageType, Resource.RemovedSuccessfully_a);

            return RedirectToAction(nameof(Index));
        }

        private bool Exists(Guid id)
        {
            return _businessObject.Exists(id);
        }
    }
}
