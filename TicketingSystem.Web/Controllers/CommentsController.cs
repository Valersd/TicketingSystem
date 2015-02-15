using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using TicketingSystem.Data;
using TicketingSystem.Models;
using TicketingSystem.Web.Models;
using TicketingSystem.Web.ViewModels;

using AutoMapper;

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
                    var userId = CurrentUser.Id;

                    Comment newComment = new Comment
                    {
                        Content = comment.Content,
                        TicketId = comment.TicketId,
                        AuthorId = userId
                    };

                    Data.Comments.Add(newComment);
                    CurrentUser.Points++;
                    Data.SaveChanges();


                    var commentModel = Mapper.Map<CommentInTicket>(newComment);
                    commentModel.Author = CurrentUser.UserName;

                    return PartialView("_CommentInTicket", commentModel);
                }
            }

            return RedirectToAction("Details", "Home", new { id = comment.TicketId });
        }
    }
}