using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TicketingSystem.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        private ICollection<Ticket> tickets;
        private ICollection<Comment> comments;

        public User()
        {
            this.Points = 10;
            this.Tickets = new HashSet<Ticket>();
            this.Comments = new HashSet<Comment>();
        }

        public int Points { get; set; }

        public virtual ICollection<Ticket> Tickets
        {
            get { return this.tickets; }
            set { this.tickets = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
