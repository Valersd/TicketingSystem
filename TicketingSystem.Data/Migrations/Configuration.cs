namespace TicketingSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using TicketingSystem.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TicketingSystem.Common;

    public sealed class Configuration : DbMigrationsConfiguration<TicketingSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(TicketingSystemDbContext context)
        {
            SeedRolesAndUsers(context);
            SeedCategoriesTicketsComments(context);
        }

        private void SeedRolesAndUsers(TicketingSystemDbContext context)
        {
            if (context.Roles.Any())
            {
                return;
            }
            var userManager = new UserManager<User>(new UserStore<User>(context));
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole(GlobalConstants.AdminRole), new IdentityRole(GlobalConstants.UserRole));
            context.SaveChanges();
            for (int i = 0; i < 10; i++)
            {
                var user = (new User
                    {
                        UserName = RandomGenerator.GetRandomString(6, 16)
                    });
                userManager.Create(user, GlobalConstants.TestPassword);
                userManager.AddToRole(user.Id, 
                    RandomGenerator.GetRandomNumber(1, 2) == 1 ? GlobalConstants.AdminRole : GlobalConstants.UserRole);
            }
            context.SaveChanges();
        }

        private void SeedCategoriesTicketsComments(TicketingSystemDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            var users = context.Users.Take(RandomGenerator.GetRandomNumber(20)).ToList();

            for (int i = 0; i < 5; i++)
            {
                Category category = new Category
                {
                    Name = RandomGenerator.GetRandomText(3,20)
                };
                int ticketsCount = RandomGenerator.GetRandomNumber(5, 20);
                for (int j = 0; j < ticketsCount; j++)
                {
                    Ticket ticket = new Ticket
                    {
                        Title = RandomGenerator.GetRandomText(3, 50),
                        Author = users[RandomGenerator.GetRandomNumber(0, users.Count - 1)],
                        ScreenshotUrl = "http://blog.jimdo.com/wp-content/uploads/2014/01/tree-247122.jpg",
                        Description = RandomGenerator.GetRandomText(100, 1000),
                    };
                    category.Tickets.Add(ticket);
                    int commentsCount = RandomGenerator.GetRandomNumber(3, 15);
                    for (int k = 0; k < commentsCount; k++)
                    {
                        Comment comment = new Comment
                        {
                            Content = RandomGenerator.GetRandomText(5, 300),
                            Author = users[RandomGenerator.GetRandomNumber(0, users.Count - 1)],
                        };
                        ticket.Comments.Add(comment);
                    }
                }
                context.Categories.AddOrUpdate(c => c.Name, category);
            }
            context.SaveChanges();
        }
    }
}
