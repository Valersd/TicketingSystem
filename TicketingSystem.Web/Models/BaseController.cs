using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketingSystem.Data;

namespace TicketingSystem.Web.Models
{
    [Authorize]
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
    }
}