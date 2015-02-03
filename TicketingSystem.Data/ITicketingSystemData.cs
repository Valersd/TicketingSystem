using System.Data.Entity;
using TicketingSystem.Models;
namespace TicketingSystem.Data
{
    interface ITicketingSystemData
    {
        IRepository<Category> Categories { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Ticket> Tickets { get; }
        IRepository<User> Users { get; }
        DbContext Context { get; }
        void Dispose();
        int SaveChanges();
    }
}
