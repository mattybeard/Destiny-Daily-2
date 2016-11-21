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
        private TrialsManager trialsManager { get; set; }

        public HomeController()
        {
            xurManager = new XurManager();
            trialsManager = new TrialsManager();
        }

        public ActionResult Index()
        {
            var active = xurManager.IsActive() || trialsManager.GetCurrentMap() != null;
            
            return View(active);
        }
    }
}