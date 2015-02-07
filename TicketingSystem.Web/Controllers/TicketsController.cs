using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketingSystem.Data;
using TicketingSystem.Web.Models;

using AutoMapper.QueryableExtensions;
using TicketingSystem.Web.ViewModels;

using PagedList;
using PagedList.Mvc;

namespace TicketingSystem.Web.Controllers
{
    public class TicketsController : BaseController
    {
        public TicketsController(ITicketingSystemData data)
            : base(data)
        {

        }

        // GET: Tickets
        public ActionResult Index(int? page, int? catId)
        {
            var categories = Data.Categories.All().ToList();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.SelectedCategory = catId;

            var tickets = Data.Tickets.All();

            if (catId != null)
            {
                tickets = tickets.Where(t => t.CategoryId == catId);
            }

            var pagableTickets = tickets
            .OrderBy(t => t.Id)
            .Project()
            .To<TicketInTicketsIndex>()
            .ToPagedList(page ?? 1, 5);

            return View(pagableTickets);
        }

        public ActionResult Search(string titlePart)
        {
            var tickets = Data.Tickets.All()
                .Where(t => t.Title.Contains(titlePart))
                .Project()
                .To<TicketInTicketsIndex>();
            return Json(tickets, JsonRequestBehavior.AllowGet);
        }
    }
}