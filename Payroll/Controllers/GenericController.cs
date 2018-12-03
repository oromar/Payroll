using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payroll.Business;
using Payroll.Common;
using Payroll.Models;
using System;
using System.Threading.Tasks;

namespace Payroll.Controllers
{
    public abstract class GenericController<T> : Controller where T : Basic
    {
        protected readonly BusinessObject<T> _businessObject;

        protected readonly Message _message;

        public GenericController(BusinessObject<T> businessObject, Message message)
        {
            _businessObject = businessObject;
            _message = message;
        }

        public void CreateMessage(string type, string message)
        {
            _message.Body = message;
            _message.Type = type;
        }

        public virtual async Task<IActionResult> Index(int page = 1, string filter = "", string sort = "", string order = "ASC")
        {
            var totalItems = await _businessObject.Count(filter);
            ViewBag.CurrentPage = page;
            ViewBag.CurrentFilter = filter;
            ViewBag.TotalItems = totalItems;
            ViewBag.HasMore = totalItems > (page * Constants.MAX_ITEMS_PER_PAGE);
            ViewBag.ItemsPerPage = Constants.MAX_ITEMS_PER_PAGE;
            ViewBag.Sort = sort;
            ViewBag.Order = order;

            if (_message.HasMessage)
            {
                ViewBag.Message = _message.Body;
                ViewBag.MessageType = _message.Type;

                _message.Clear();
            }

            return View(await _businessObject.Search(page, filter, sort, order));
        }

        public virtual async Task<IActionResult> Details(Guid? id)
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

        public virtual IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Create([Bind] T data)
        {
            if (ModelState.IsValid)
            {
                await _businessObject.Create(data, User.Identity.Name);

                CreateMessage(Resource.SuccessMessageType, Resource.CreatedSuccessfully);

                return RedirectToAction(nameof(Index));
            }
            else 
            {
                CreateMessage(Resource.DangerMessageType, Resource.InvalidData);
            }
            return View(data);
        }

        public virtual async Task<IActionResult> Edit(Guid? id)
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
        public virtual async Task<IActionResult> Edit(Guid id, [Bind] T data)
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

                    CreateMessage(Resource.SuccessMessageType, Resource.UpdatedSuccessfully);
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

                        return RedirectToAction(nameof(Index));
                    }
                }               
            } 
            else 
            {
                CreateMessage(Resource.DangerMessageType, @Resource.InvalidData);
            }
            return RedirectToAction(nameof(Index));
        }

        public virtual async Task<IActionResult> Delete(Guid? id)
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
        public virtual async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _businessObject.Delete(id, User.Identity.Name);

            CreateMessage(Resource.SuccessMessageType, Resource.RemovedSuccessfully);

            return RedirectToAction(nameof(Index));
        }

        private bool Exists(Guid id)
        {
            return _businessObject.Exists(id);
        }
    }
}
