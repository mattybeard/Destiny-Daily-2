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
        private DateTime StandardDate => DateTime.Now.AddHours(-9.0).AddMinutes(2);
        private HeroicDailyModel cache { get; set; }
        private bool CacheExpired
        {
            get
            {
                if (cache == null)
                    return true;

                if (cache.ExpiryTime < DateTime.Now)
                    return true;

                if(DateTime.Now.Hour == 9 && cache.ExpiryTime.Hour == 9)
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
                var daily = DailyManager.GetDaily(StandardDate);
                cache = new HeroicDailyModel() { DailyMission = daily, DailyBounties = new List<BountyDay>() };
                if (daily != null)
                {
                    cache.DailyModifiers = DailyManager.GetModifiers(daily.missionid);
                    cache.DailyRewards = DailyManager.GetRewards(daily.missionid);
                }
                ;

                var dailyCruc = DailyManager.GetDailyCrucible(StandardDate);
                if (dailyCruc != null)
                {
                    cache.DailyCrucible = dailyCruc;
                    cache.DailyCrucibleRewards = DailyManager.GetRewards(dailyCruc.activityid);
                }
                var bounties = BountyManager.GetBounties(StandardDate, 1);
                if (bounties != null && bounties.Any())
                    cache.DailyBounties = bounties;

                cache.DisplayDate = StandardDate;
                cache.ExpiryTime = DateTime.Now.AddHours(1);
            }

            if (noLayout)
                return View("PartialIndex", cache);
            else
            {
                ViewBag.HtmlTagOverride = @"data-redirect=""/#daily""";
                return View("Index", cache);
            }
        }
    }
}