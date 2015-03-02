using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketingSystem.Models;

namespace TicketingSystem.Web.Areas.Admin.ViewModels
{
    public class TicketInUser
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public PriorityType Priority { get; set; }
    }
}