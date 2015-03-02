using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketingSystem.Web.Areas.Admin.ViewModels
{
    public class UserDetails
    {
        public string Id { get; set; }

        [Display(Name="User Name")]
        public string Username { get; set; }

        public IEnumerable<TicketInUser> Tickets { get; set; }

        public IEnumerable<CommentInUser> Comments { get; set; }
    }
}