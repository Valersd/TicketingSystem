using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TicketingSystem.Models;

namespace TicketingSystem.Data
{
    public class TicketingSystemDbContext : IdentityDbContext<User>
    {
        public TicketingSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Category> Categories { get; set; }
        public virtual IDbSet<Ticket> Tickets { get; set; }
        public virtual IDbSet<Comment> Comments { get; set; }

        public static TicketingSystemDbContext Create()
        {
            return new TicketingSystemDbContext();
        }

    }
}
