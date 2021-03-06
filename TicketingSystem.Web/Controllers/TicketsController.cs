﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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
                .Include(t => t.Category)
                .Include(t => t.Comments)
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
            return Json(tickets.AsEnumerable(), JsonRequestBehavior.AllowGet);
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
                ticket.Title = UppercaseFirstLetter(ticket.Title);
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
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
                .ToList();

            ViewBag.Categories = categories;
        }

        private string UppercaseFirstLetter(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            char[] array = text.ToCharArray();
            array[0] = Char.ToUpper(array[0]);
            return new string(array);
        }
    }
}