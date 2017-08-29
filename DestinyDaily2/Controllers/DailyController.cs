using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Web.Mvc;
using DestinyDaily2.Models;
using DestinyDailyDAL;

namespace DestinyDaily2.Controllers
{
    public class DailyController : Controller
    {
        private DailyManager DailyManager { get; }
        private BountyManager BountyManager { get; }
        private static HeroicDailyModel Cache { get; set; }
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
        public DailyController()
        {
            DailyManager = new DailyManager();
            BountyManager = new BountyManager();
        }

        public ActionResult Index(bool noLayout = false)
        {
            if (CacheExpired)
            {
                Cache = new HeroicDailyModel
                {
                    DailyMission = DailyManager.GetDaily()
                };

                if (Cache.DailyMission != null)
                {
                    Cache.DailyModifiers = DailyManager.GetModifiers(Cache.DailyMission.missionid);
                    Cache.DailyRewards = DailyManager.GetRewards(Cache.DailyMission.missionid);
                }                

                var bounties = BountyManager.GetBounties();
                if (bounties != null && bounties.Any())
                    Cache.DailyBounties = bounties;

                Cache.TimeDifferenceTime = DailyManager.TodayDate;
                Cache.ExpiryTime = DateTime.Now.AddHours(1);
                Cache.StartTime = DateTime.Now;
            }

            if (noLayout)
                return View("PartialIndex", Cache);
            else
            {
                ViewBag.HtmlTagOverride = @"data-redirect=""/#daily""";
                return View("Index", Cache);
            }
        }
    }
}