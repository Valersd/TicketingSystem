using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketingSystem.Web.Areas.Admin.Models
{
    public class CommentEdit
    {
        public int Id { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 5)]
        public string Content { get; set; }
    }
}