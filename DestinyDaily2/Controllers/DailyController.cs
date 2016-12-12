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
        public DailyController()
        {
            DailyManager = new DailyManager();
            BountyManager = new BountyManager();
        }

        public ActionResult Index(bool noLayout = false)
        {
            var daily = DailyManager.GetDaily(StandardDate);
            var dailyStructure = new HeroicDailyModel() {DailyMission = daily, DailyBounties = new List<BountyDay>() };
            if (daily != null)
            {
                dailyStructure.DailyModifiers = DailyManager.GetModifiers(daily.missionid);
                dailyStructure.DailyRewards = DailyManager.GetRewards(daily.missionid);
            };

            var dailyCruc = DailyManager.GetDailyCrucible(StandardDate);
            if (dailyCruc != null)
            {
                dailyStructure.DailyCrucible = dailyCruc;
                dailyStructure.DailyCrucibleRewards = DailyManager.GetRewards(dailyCruc.activityid);
            }
            var bounties = BountyManager.GetBounties(StandardDate, 1);
            if (bounties != null && bounties.Any())
                dailyStructure.DailyBounties = bounties;

            dailyStructure.DisplayDate = StandardDate;

            if (noLayout)
                return View("PartialIndex", dailyStructure);
            else
            {
                ViewBag.HtmlTagOverride = @"data-redirect=""/#daily""";
                return View("Index", dailyStructure);
            }
        }
    }
} 