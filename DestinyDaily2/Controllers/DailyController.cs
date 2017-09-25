using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Web.Mvc;
using DestinyDaily2.Models;
using DestinyDaily2.Models.Destiny1;
using DestinyDailyDAL;

namespace DestinyDaily2.Controllers
{
    public class DailyController : Controller
    {
        private DailyManager DailyManager { get; }
        private BountyManager BountyManager { get; }
        private static HeroicDailyModel Destiny1Cache { get; set; }
        private static DailyModel Cache { get; set; }

        public DailyController()
        {
            DailyManager = new DailyManager();
            BountyManager = new BountyManager();
        }

        public ActionResult Index(bool noLayout = false)
        {
            Cache = new DailyModel();
            BountyManager.SampleApiTest();

            if (noLayout)
                return View("PartialIndex", Cache);

            ViewBag.HtmlTagOverride = @"data-redirect=""/#daily""";
            return View("Index", Cache);
        }

        public ActionResult Destiny1Index(bool noLayout = false)
        {
            if (Destiny1Cache == null || Destiny1Cache.CacheExpired)
            {
                Destiny1Cache = new HeroicDailyModel
                {
                    DailyMission = DailyManager.GetDaily()
                };

                if (Destiny1Cache.DailyMission != null)
                {
                    Destiny1Cache.DailyModifiers = DailyManager.GetModifiers(Destiny1Cache.DailyMission.missionid);
                    Destiny1Cache.DailyRewards = DailyManager.GetRewards(Destiny1Cache.DailyMission.missionid);
                }                

                var bounties = BountyManager.GetBounties();
                if (bounties != null && bounties.Any())
                    Destiny1Cache.DailyBounties = bounties;

                Destiny1Cache.TimeDifferenceTime = DailyManager.TodayDate;
                Destiny1Cache.ExpiryTime = DateTime.Now.AddHours(1);
                Destiny1Cache.StartTime = DateTime.Now;
            }

            if (noLayout)
                return View("Destiny1/PartialIndex", Destiny1Cache);

            ViewBag.HtmlTagOverride = @"data-redirect=""/#daily""";
            return View("Destiny1/Index", Cache);
        }
    }
}