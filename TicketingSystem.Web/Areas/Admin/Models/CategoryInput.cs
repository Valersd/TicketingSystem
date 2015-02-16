using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketingSystem.Web.Areas.Admin.Models
{
    public class CategoryInput
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }
    }
}