using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using TicketingSystem.Models;
using TicketingSystem.Web.Common;

namespace TicketingSystem.Web.Models
{
    public class TicketInput
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        [DoNotContain("bug")]
        public string Title { get; set; }

        public PriorityType Priority { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(100)]
        [Display(Name="Screenshot Url")]
        public string ScreenshotUrl { get; set; }

        [Display(Name="Category")]
        public int CategoryId { get; set; }
    }
}