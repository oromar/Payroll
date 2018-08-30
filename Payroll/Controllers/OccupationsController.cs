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
        private readonly OccupationsBO _occupationsBO;
        private readonly Message _message;
        
        public OccupationsController(OccupationsBO occupationsBO, Message message)
        {
            _occupationsBO = occupationsBO;
            _message = message;
        }

        
        public async Task<IActionResult> Index(int page = 1, string filter = "")
        {
            ViewData["CurrentPage"] = page;
            ViewData["CurrentFilter"] = filter;
            ViewData["HasMore"] = await _occupationsBO.HasMore(page, filter);

            if (_message.HasMessage)
            {
                ViewBag.MessageTitle = _message.Title;
                ViewBag.Message = _message.Body;
                ViewBag.MessageType = _message.Type;

                _message.Clear();
            }

            return View(await _occupationsBO.Search(page, filter));
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occupation = await _occupationsBO.Find((Guid)id);

            if (occupation == null)
            {
                return NotFound();
            }

            return View(occupation);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsRegulated,CouncilName")] Occupation occupation)
        {
            if (ModelState.IsValid)
            {
                await _occupationsBO.Create(occupation, User.Identity.Name);

                CreateMessage(Resource.CreatedSuccessfully_a, Resource.SuccessMessageType);

                return RedirectToAction(nameof(Index));
            }
            return View(occupation);
        }

        private void CreateMessage(string message, string type)
        {
            _message.Title = Resource.Occupation;
            _message.Body = message;
            _message.Type = type;
        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occupation = await _occupationsBO.Find((Guid)id);

            if (occupation == null)
            {
                return NotFound();
            }
            return View(occupation);
        }

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
                    await _occupationsBO.Edit(id, occupation, User.Identity.Name);

                    CreateMessage(Resource.UpdatedSuccessfully_a, Resource.SuccessMessageType);
                }
                catch (DbUpdateConcurrencyException e) 
                {
                    if (!OccupationExists(occupation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        CreateMessage(e.Message, Resource.DangerMessageType);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(occupation);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occupation = await _occupationsBO.Find((Guid)id);     
            
            if (occupation == null)
            {
                return NotFound();
            }

            return View(occupation);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            await _occupationsBO.Delete(id, User.Identity.Name);

            CreateMessage(Resource.RemovedSuccessfully_a, Resource.SuccessMessageType);

            return RedirectToAction(nameof(Index));
        }

        private bool OccupationExists(Guid id)
        {
            return _occupationsBO.Exists(id);
        }
    }
}
