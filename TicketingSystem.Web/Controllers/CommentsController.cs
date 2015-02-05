using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketingSystem.Data;
using TicketingSystem.Models;
using TicketingSystem.Web.Models;
using Microsoft.AspNet.Identity;
using AutoMapper;
using TicketingSystem.Web.ViewModels;

namespace TicketingSystem.Web.Controllers
{
    public class CommentsController : BaseController
    {
        public CommentsController(ITicketingSystemData data)
            : base(data)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentInput comment)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    Comment newComment = new Comment
                    {
                        Content = comment.Content,
                        TicketId = comment.Id,
                        AuthorId = User.Identity.GetUserId()
                    };

                    Data.Comments.Add(newComment);
                    Data.SaveChanges();

                    var commentModel = Mapper.Map<CommentInTicket>(newComment);
                    commentModel.Author = User.Identity.Name;

                    return PartialView("_CommentInTicket", commentModel);
                }
            }

            return RedirectToAction("Details", "Home", new { id = comment.Id });
        }
    }
}