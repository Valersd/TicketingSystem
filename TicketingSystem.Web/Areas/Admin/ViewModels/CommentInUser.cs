using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketingSystem.Web.Areas.Admin.ViewModels
{
    public class CommentInUser
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int TicketId { get; set; }

        public string Ticket { get; set; }
    }
}