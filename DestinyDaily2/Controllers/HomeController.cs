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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Destiny1()
        {
            return View();
        }
    }
}