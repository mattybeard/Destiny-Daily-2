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
        private BountyManager BountyManager { get; set; }
        private DateTime StandardDate => DateTime.Now.AddHours(-9.0);

        public HomeController()
        {
            BountyManager = new BountyManager();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}