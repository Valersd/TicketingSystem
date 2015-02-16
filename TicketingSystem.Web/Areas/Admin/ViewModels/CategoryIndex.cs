using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketingSystem.Web.Areas.Admin.ViewModels
{
    public class CategoryIndex
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name="Count of tickets")]
        public int TicketsCount { get; set; }
    }
}