using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketingSystem.Models
{
    public class Category
    {
        private ICollection<Ticket> tickets;

        public Category()
        {
            this.Tickets = new HashSet<Ticket>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20,MinimumLength=3)]
        public string Name { get; set; }

        public virtual ICollection<Ticket> Tickets
        {
            get { return this.tickets; }
            set { this.tickets = value; }
        }
    }
}
