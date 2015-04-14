using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TicketingSystem.Models;

namespace TicketingSystem.Data
{
    public class TicketingSystemDbContext : IdentityDbContext<User>
    {
        public TicketingSystemDbContext()
            : base("TicketingSystem", throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
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
