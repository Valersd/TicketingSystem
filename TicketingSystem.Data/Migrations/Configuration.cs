namespace TicketingSystem.Data.Migrations
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Faker;

    using TicketingSystem.Common;
    using TicketingSystem.Models;

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
            context.Roles.AddOrUpdate(r => r.Name, 
                new IdentityRole(GlobalConstants.AdminRole), 
                new IdentityRole(GlobalConstants.UserRole),
                new IdentityRole(GlobalConstants.InactiveRole));
            context.SaveChanges();

            var testAdmin = new User { UserName = "testadmin" };
            userManager.Create(testAdmin, GlobalConstants.TestPassword);
            userManager.AddToRole(testAdmin.Id, GlobalConstants.AdminRole);

            var testUser = new User { UserName = "testuser" };
            userManager.Create(testUser, GlobalConstants.TestPassword);
            userManager.AddToRole(testUser.Id, GlobalConstants.UserRole);

            for (int i = 0; i < 15; i++)
            {
                var user = new User { UserName = Internet.UserName() };
                userManager.Create(user, GlobalConstants.TestPassword);
                userManager.AddToRole(user.Id, 
                    RandomGenerator.GetRandomNumber() >= 66 ? GlobalConstants.AdminRole : GlobalConstants.UserRole);
            }
            context.SaveChanges();
        }

        private void SeedCategoriesTicketsComments(TicketingSystemDbContext context)
        {
            if (context.Categories.Any())
            {
                return;
            }

            var users = context.Users.ToList();
            string[] urls = new string[]
            {
                "http://gcc-python-plugin.readthedocs.org/en/latest/_images/sample-html-error-report.png",
                "http://a3li.li/wp-content/uploads/2013/11/bugzie-prod-select-2.png",
                "https://hoopercharles.files.wordpress.com/2012/09/topicofprogramming2-12.jpg",
                "http://rules.ssw.com.au/Communication/RulesToBetterEmail/PublishingImages/emailbugreport_good.gif",
                "http://www.symantec.com/business/support/library/BUSINESS/ATLAS/images_v1/316952/mseo.jpg",
                "http://www.symantec.com/business/support/library/BUSINESS/ATLAS/images_v1/283798/inc_issue2.jpg"
            };

            for (int i = 0; i < 7; i++)
            {
                Category category = new Category
                {
                    Name = Lorem.Words(1).ToList()[0].ToUpper()
                };
                context.Categories.Add(category);
                context.SaveChanges();

                int ticketsCount = RandomGenerator.GetRandomNumber(5, 20);
                for (int j = 0; j < ticketsCount; j++)
                {
                    var paragraphCount = RandomGenerator.GetRandomNumber(2, 3);
                    string description = String.Empty;
                    for (int p = 0; p < paragraphCount; p++)
                    {
                        string paragraph = String.Join(" ", Lorem.Paragraphs(RandomGenerator.GetRandomNumber(2, 4)));
                        description += paragraph + Environment.NewLine;
                    }
                    description += String.Join(" ", Lorem.Paragraphs(RandomGenerator.GetRandomNumber(1, 3)));

                    string sentence = Lorem.Sentence();
                    if (sentence.Length > 50)
                    {
                        sentence = sentence.Substring(0, 50);
                    }

                    Ticket ticket = new Ticket
                    {
                        Title = sentence,
                        CategoryId = category.Id,
                        AuthorId = users[RandomGenerator.GetRandomNumber(0, users.Count - 1)].Id,
                        ScreenshotUrl = urls[RandomGenerator.GetRandomNumber(0, urls.Length - 1)],
                        Description = description,
                        Priority = GetRandomPriority(RandomGenerator.GetRandomNumber())
                    };
                    context.Tickets.Add(ticket);
                    context.SaveChanges();

                    int commentsCount = RandomGenerator.GetRandomNumber(3, 15);
                    for (int k = 0; k < commentsCount; k++)
                    {
                        User author = users[RandomGenerator.GetRandomNumber(0, users.Count - 1)];

                        Comment comment = new Comment
                        {
                            Content = Lorem.Paragraph(2),
                            AuthorId = author.Id,
                            TicketId = ticket.Id
                        };
                        author.Points++;
                        context.Comments.Add(comment);
                        context.SaveChanges();
                    }
                }
            }
            context.SaveChanges();
        }

        private PriorityType GetRandomPriority(int number)
        {
            if (number < 33)
            {
                return PriorityType.Low;
            }
            else if (number >= 33 && number < 67)
            {
                return PriorityType.Medium;
            }
            else
            {
                return PriorityType.High;
            }
        }
    }
}
