using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Models;

namespace TicketingSystem.Data
{
    public class TicketingSystemDbContext : IdentityDbContext<User>
    {
        public TicketingSystemDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static TicketingSystemDbContext Create()
        {
            return new TicketingSystemDbContext();
        }
    }
}
