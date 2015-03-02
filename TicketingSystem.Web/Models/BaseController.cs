using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketingSystem.Data;
using TicketingSystem.Models;

namespace TicketingSystem.Web.Models
{
    [Authorize(Roles="Admin, User")]
    [ValidateInput(false)]
    public class BaseController : Controller
    {
        private ITicketingSystemData _data;


        public BaseController(ITicketingSystemData data)
        {
            _data = data;
        }

        

        //public BaseController():
        //    this(new TicketingSystemData(new TicketingSystemDbContext()))
        //{

        //}

        public ITicketingSystemData Data
        {
            get
            {
                return _data;
            }
        }

        public User CurrentUser
        {
            get
            {
                return GetCurrentUser();
            }
        }

        private User GetCurrentUser()
        {
            return Data.Users.GetById(User.Identity.GetUserId());
            //var userManager = new UserManager<User>(new UserStore<User>(Data.Context));
            //return userManager.FindById(User.Identity.GetUserId());
        }
    }
}