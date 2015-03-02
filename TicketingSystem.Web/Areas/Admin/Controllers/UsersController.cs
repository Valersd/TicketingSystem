using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using TicketingSystem.Data;
using TicketingSystem.Common;
using TicketingSystem.Models;
using TicketingSystem.Web.Models;
using TicketingSystem.Web.Areas.Admin.ViewModels;
using TicketingSystem.Web.Areas.Admin.Models;

using PagedList;
using PagedList.Mvc;

using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Net;

namespace TicketingSystem.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : BaseController
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UsersController(ITicketingSystemData data, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
            : base(data)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        // GET: Admin/Users
        [OutputCache(Duration = 0, NoStore = true)]
        public ActionResult Index(int? page)
        {
            //IdentityRole adminRole = _roleManager.FindByName(GlobalConstants.AdminRole);

            //var roles = _roleManager.Roles.ToList();

            var roles = Data.Roles.All();
            IdentityRole adminRole = roles.FirstOrDefault(r => r.Name == GlobalConstants.AdminRole);
            var usersExceptAdmin = Data.Users.All().Where(u => u.Roles.All(r => r.RoleId != adminRole.Id));

            var users = (from u in usersExceptAdmin
                         from r in roles.Where(r => r.Users.Select(x => x.UserId).Contains(u.Id))
                         select new UserIndex
                         {
                             Id = u.Id,
                             UserName = u.UserName,
                             Points = u.Points,
                             Role = r.Name
                         })
                      .OrderBy(u => u.UserName)
                      .ToPagedList(page ?? 1, 5);

            //var users = Data.Users.All()
            //    .Include(u => u.Roles)
            //    .Where(u => u.Roles.All(r => r.RoleId != adminRole.Id))
            //    .OrderBy(x => x.UserName)
            //    //.AsEnumerable()
            //    .Select(u => new UserIndex
            //    {
            //        Id = u.Id,
            //        UserName = u.UserName,
            //        Points = u.Points,
            //        //Role = String.Join(", ", from ur in u.Roles
            //        //                         join r in roles on ur.RoleId equals r.Id
            //        //                         select r.Name),

            //        //Role = String.Join(", ", u.Roles.Join(roles, ur => ur.RoleId, rr => rr.Id, (ur, rr) => rr.Name))

            //        Role = u.Roles.Join(roles, ur => ur.RoleId, rr => rr.Id, (ur, rr) => rr.Name).FirstOrDefault()

            //    })
            //    .ToPagedList(page ?? 1, 5);

            return View(users);
        }

        // GET: Admin/Users/Details/5
        public ActionResult Details(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string decryptedId = Cipher.Decrypt(userId);

            var user = Data.Users.All()
                .Where(u => u.Id == decryptedId)
                .Include(u=>u.Tickets)
                .Include(u=>u.Comments)
                .Project()
                .To<UserDetails>()
                .FirstOrDefault();

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }


        [HttpGet]
        public ActionResult Edit(string userId)
        {
            if (String.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string decryptedId = Cipher.Decrypt(userId);

            //var user = _userManager.FindById(decryptedId);
            //var user = Data.Users.GetById(decryptedId);

            var roles = Data.Roles.All().AsEnumerable();
            var model = Data.Users.All()
                .Where(u => u.Id == decryptedId)
                .Include(u => u.Roles)
                .Select(user => new UserEditRole
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    //Role = _userManager.GetRoles(user.Id).FirstOrDefault()
                    Role = roles.Join(user.Roles, rr => rr.Id, ru => ru.RoleId, (rr, ru) => rr.Name).FirstOrDefault(),
                    Roles = roles.Select(r => r.Name)
                })
                .FirstOrDefault();

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        // POST: Admin/Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditRole user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _userManager.FindById(user.Id).Roles.Clear();
                    _userManager.AddToRole(user.Id, user.Role);

                    TempData["message"] = "Role successfully updated";
                    TempData["color"] = "alert-success";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["message"] = "An error occured";
                    TempData["color"] = "alert-danger";
                    return RedirectToAction("Edit", new { userId = Cipher.Encrypt(user.Id) });
                }
            }


            return View(user);
        }
    }
}
