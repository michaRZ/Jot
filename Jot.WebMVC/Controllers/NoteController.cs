using Jot.Models;
using Jot.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Jot.WebMVC.Controllers
{
    [Authorize]
    public class NoteController : Controller
    {
        // GET: Note
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            var model = service.GetNotes();

            return View(model);
        }

        // GET: Note/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Note/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(NoteCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateNoteService();

            if (service.CreateNote(model))
            {
                TempData["SaveResult"] = "Your new note has been created!";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Uh-oh. Looks like your note could not be created. Check your entry and try again.");

            return View(model);
        }

        // GET: Note/Details/{id}
        public ActionResult Details(int id)
        {
            var service = CreateNoteService();
            var model = service.GetNoteById(id);

            return View(model);
        }

        // GET: Note/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = CreateNoteService();
            var detail = service.GetNoteById(id);
            var model = new NoteEdit
            {
                NoteId = detail.NoteId,
                Title = detail.Title,
                Content = detail.Content
            };

            return View(model);
        }

        // POST: Note/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, NoteEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.NoteId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateNoteService();

            if (service.UpdateNote(model))
            {
                TempData["SaveResult"] = "Note updated!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Uh-oh! Your note could not be updated. Check your entry and try again.");
            return View(model);
        }

        // GET: Note/Delete/{id}
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var service = CreateNoteService();
            var model = service.GetNoteById(id);

            return View(model);
        }

        // POST: Note/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var service = CreateNoteService();

            service.DeleteNote(id);

            TempData["SaveResult"] = "Your note was deleted";

            return RedirectToAction("Index");
        }

        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new NoteService(userId);
            return service;
        }
    }
}