using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.Models
{
    public class Ticket
    {
        private ICollection<Comment> comments;

        public Ticket()
        {
            this.Comments = new HashSet<Comment>();
            this.Priority = PriorityType.Medium;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50,MinimumLength=3)]
        public string Title { get; set; }

        public PriorityType Priority { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [StringLength(100)]
        public string ScreenshotUrl { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

    }
}
