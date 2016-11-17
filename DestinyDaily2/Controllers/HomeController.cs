using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DestinyDaily2.Models;
using DestinyDailyApiManager;
using DestinyDailyApiManager.Models;
using DestinyDailyDAL;

namespace DestinyDaily2.Controllers
{
    public class HomeController : Controller
    {
        private XurManager xurManager { get; set; }

        public HomeController()
        {
            xurManager = new XurManager();
        }

        public ActionResult Index()
        {
            return View(xurManager.IsActive());
        }
    }
}