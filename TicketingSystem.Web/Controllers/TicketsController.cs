using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using PagedList;
using PagedList.Mvc;

using TicketingSystem.Models;
using TicketingSystem.Data;
using TicketingSystem.Web.Models;
using TicketingSystem.Web.ViewModels;


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
            .OrderByDescending(t => t.Id)
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

        [HttpGet]
        public ActionResult Create()
        {
            PopulateCategories();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TicketInput ticket)
        {
            if (ModelState.IsValid)
            {
                Ticket newTicket = Mapper.Map<Ticket>(ticket);
                newTicket.AuthorId = User.Identity.GetUserId();

                Data.Tickets.Add(newTicket);
                Data.SaveChanges();

                return RedirectToAction("Details", "Home", new { id = newTicket.Id, area = "" });
            }

            PopulateCategories();

            return View(ticket);
        }

        private void PopulateCategories()
        {
            var categories = Data.Categories.All()
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
                .ToList();

            ViewBag.Categories = categories;
        }
    }
}