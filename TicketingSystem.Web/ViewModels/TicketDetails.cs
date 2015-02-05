using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TicketingSystem.Models;

namespace TicketingSystem.Web.ViewModels
{
    public class TicketDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ScreenshotUrl { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public PriorityType Priority { get; set; }
        public ICollection<CommentInTicket> Comments { get; set; }

    }
}