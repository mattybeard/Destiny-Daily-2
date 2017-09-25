using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DestinyDaily2.Models;
using DestinyDaily2.Models.Destiny1;
using DestinyDailyDAL;

namespace DestinyDaily2.Controllers
{
    public class NightFallController : Controller
    {
        public NightFallManager NfManager { get; set; }
        public BountyManager BountyManager { get; set; }
        private static NightfallDataModel Destiny1Cache { get; set; }
        private static NightfallModel Cache { get; set; }
        public NightFallController()
        {
            NfManager = new NightFallManager();
            BountyManager = new BountyManager();
        }
        
        public ActionResult Destiny1Index(bool noLayout = false)
        {
            if (Destiny1Cache == null || Destiny1Cache.CacheExpired)
            {

                var nf = NfManager.GetD1NightFall();
                var weeklyBounties = BountyManager.GetWeeklyBounties("Zavala");

                Destiny1Cache = new NightfallDataModel()
                {
                    ThisNightfall = nf,
                    ExpiryTime = DateTime.Now.AddHours(1),
                    StartTime = DateTime.Now,
                    WeeklyStrikeBounties = weeklyBounties
                };
            }

            if (noLayout)
                return View("Destiny1/PartialIndex", Destiny1Cache);
            else
            {
                ViewBag.HtmlTagOverride = @"data-redirect=""/#nightfall""";
                return View("Destiny1/Index", Destiny1Cache);
            }
        }

        public ActionResult Index(bool noLayout = false)
        {
            if (Cache == null || Cache.CacheExpired)
            {
                if(Cache == null)
                    Cache = new NightfallModel();

                Cache.Nightfall = NfManager.GetD2NightFall();
            }

            if (noLayout)
                return View("PartialIndex", Cache);
            else
            {
                ViewBag.HtmlTagOverride = @"data-redirect=""/#nightfall""";
                return View("Index", Cache);
            }
        }
    }
}