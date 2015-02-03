using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(300,MinimumLength=5)]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public int TicketId { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
