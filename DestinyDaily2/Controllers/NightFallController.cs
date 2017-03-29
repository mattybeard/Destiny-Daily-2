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
        public BountyManager bountyManager { get; set; }
        private static NightfallDataModel cache { get; set; }
        private bool CacheExpired
        {
            get
            {
                if (cache == null)
                    return true;

                if (cache.ExpiryTime < DateTime.Now)
                    return true;

                if (DateTime.Now.Hour == 9 && cache.ExpiryTime.Hour == 9)
                    return true;

                return false;
            }
        }
        private DateTime StandardDate
        {
            get
            {
                var today = DateTime.Now.AddHours(-10.0).AddMinutes(2);
                while (today.DayOfWeek != DayOfWeek.Tuesday)
                {
                    today = today.AddDays(-1);
                }

                return today;
            }
        }

        public NightFallController()
        {
            NfManager = new NightFallManager();
            bountyManager = new BountyManager();
        }
        
        public ActionResult Index(bool noLayout = false)
        {
            if (CacheExpired)
            {

                var nf = NfManager.GetNightFall(StandardDate);
                var weeklyBounties = bountyManager.GetBounties(StandardDate, 1, "Zavala");

                cache = new NightfallDataModel()
                {
                    ThisDate = StandardDate,
                    ThisNightfall = nf,
                    ExpiryTime = DateTime.Now.AddHours(1),
                    StartTime = DateTime.Now,
                    WeeklyStrikeBounties = weeklyBounties
                };
            }

            if (noLayout)
                return View("PartialIndex", cache);
            else
            {
                ViewBag.HtmlTagOverride = @"data-redirect=""/#nightfall""";
                return View("Index", cache);
            }
        }
    }
}