using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TicketingSystem.Models;

namespace TicketingSystem.Data
{
    public interface ITicketingSystemData
    {
        IRepository<Category> Categories { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Ticket> Tickets { get; }
        IRepository<User> Users { get; }
        IRepository<IdentityRole> Roles { get; }
        DbContext Context { get; }
        void Dispose();
        int SaveChanges();
    }
}
