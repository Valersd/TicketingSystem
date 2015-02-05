using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketingSystem.Web.ViewModels
{
    public class CommentInTicket
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
    }
}