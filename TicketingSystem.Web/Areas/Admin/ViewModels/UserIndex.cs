using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketingSystem.Web.Areas.Admin.ViewModels
{
    public class UserIndex
    {
        public string Id { get; set; }

        [Display(Name="User Name")]
        public string UserName { get; set; }

        public string Role { get; set; }

        public int Points { get; set; }
    }
}