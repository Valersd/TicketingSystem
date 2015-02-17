using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketingSystem.Web.Areas.Admin.ViewModels
{
    public class CommentIndex
    {
        public int Id { get; set; }

        public string Content { get; set; }

        [Display(Name="Ticket")]
        public string TicketTitle { get; set; }

        [Display(Name="Author")]
        public string AuthorName { get; set; }
    }
}