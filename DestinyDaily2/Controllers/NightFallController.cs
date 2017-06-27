using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DestinyDaily2.Models;
using DestinyDailyDAL;

namespace DestinyDaily2.Controllers
{
    public class NightFallController : Controller
    {
        public NightFallManager NfManager { get; set; }
        public BountyManager BountyManager { get; set; }
        private static NightfallDataModel Cache { get; set; }
        private bool CacheExpired
        {
            get
            {
                if (Cache == null)
                    return true;

                if (Cache.ExpiryTime < DateTime.Now)
                    return true;

                if (Cache.ExpiryTime.Hour < DateTime.Now.Hour)
                    return true;

                return false;
            }
        }
        public NightFallController()
        {
            NfManager = new NightFallManager();
            BountyManager = new BountyManager();
        }
        
        public ActionResult Index(bool noLayout = false)
        {
            if (CacheExpired)
            {

                var nf = NfManager.GetNightFall();
                var weeklyBounties = BountyManager.GetWeeklyBounties("Zavala");

                Cache = new NightfallDataModel()
                {
                    ThisNightfall = nf,
                    ExpiryTime = DateTime.Now.AddHours(1),
                    StartTime = DateTime.Now,
                    WeeklyStrikeBounties = weeklyBounties
                };
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