using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TicketingSystem.Models;

namespace TicketingSystem.Web.ViewModels
{
    public class TicketInTicketsIndex
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryName { get; set; }

        public string AuthorName { get; set; }

        public PriorityType Priority { get; set; }
    }
}