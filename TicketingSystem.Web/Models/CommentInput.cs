using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketingSystem.Web.Models
{
    public class CommentInput
    {
        [Required(ErrorMessage="Comment cannot be empty")]
        [StringLength(1000, MinimumLength = 5,ErrorMessage="Comment must be between {2} and {1} characters")]
        [Display(Name="Add Comment")]
        public string Content { get; set; }

        public int TicketId { get; set; }

    }
}