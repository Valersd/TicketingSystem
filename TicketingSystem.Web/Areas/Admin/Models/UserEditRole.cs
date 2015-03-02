using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketingSystem.Web.Areas.Admin.Models
{
    public class UserEditRole
    {
        public string Id { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        public string Role { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}