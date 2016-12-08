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
        private XurManager xurManager { get; }
        private TrialsManager trialsManager { get; }
        private BountyManager bountyManager { get; }

        public HomeController()
        {
            xurManager = new XurManager();
            trialsManager = new TrialsManager();
            bountyManager = new BountyManager(); 
        }

        public ActionResult Index()
        {
            var active = xurManager.IsActive() || trialsManager.GetCurrentMap() != null || bountyManager.HasIronBannerBounties() ;
            
            return View(active);
        }
    }
}