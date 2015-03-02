using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TicketingSystem.Data;
using TicketingSystem.Web.Models;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using PagedList;
using TicketingSystem.Web.Areas.Admin.ViewModels;
using System.Net;
using TicketingSystem.Web.Areas.Admin.Models;
using TicketingSystem.Models;

namespace TicketingSystem.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCommentsController : BaseController
    {
        public AdminCommentsController(ITicketingSystemData data)
            :base(data)
        {
        }


        // GET: Admin/Comments
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult Index(int? page)
        {
            var comments = Data.Comments.All()
                .OrderByDescending(c => c.Id)
                .Project().To<CommentIndex>()
                .ToPagedList(page ?? 1, 10);

            return View(comments);
        }

        // GET: Admin/Comments/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var comment = Data.Comments.GetById(id); //before
            //var comment = Data.Comments.All().FirstOrDefault(c => c.Id == id);

            if (comment == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<CommentEdit>(comment);

            return View(model);
        }

        // POST: Admin/Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CommentEdit comment)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var editComment = Mapper.Map<Comment>(comment);

                    Data.Comments.Update(editComment,"AuthorId","TicketId");

                    //var editComment = Data.Comments.GetById(comment.Id);
                    //editComment.Content = comment.Content;
                    Data.SaveChanges();

                    TempData["message"] = "Comment successfully edited";
                    TempData["color"] = "alert-success";
                    return RedirectToAction("Index");
                }

                return View(comment);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred.Try again later");
                return View(comment);
            }
        }

        // GET: Admin/Comments/Delete/5
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(id);
        }

        // POST: Admin/Comments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Data.Comments.Delete(id);
                Data.SaveChanges();
                TempData["message"] = "Comment successfully deleted";
                TempData["color"] = "alert-success";
            }
            catch
            {
                TempData["message"] = "Some problem appears.Maybe comment is already deleted";
                TempData["color"] = "alert-danger";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
