using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DestinyDaily2.Models;
using DestinyDailyDAL;

namespace DestinyDaily2.Controllers
{
    public class WeeklyController : Controller
    {
        private VendorManager vendorManager { get; }
        private PrisonManager prisonManager { get; }
        private BountyManager bountyManager { get; }
        private DateTime TodayDate => DateTime.Now.AddHours(-9.0).AddMinutes(2);

        private DateTime StandardDate
        {
            get
            {
                var today = DateTime.Now.AddHours(-9.0).AddMinutes(2);
                while (today.DayOfWeek != DayOfWeek.Tuesday)
                {
                    today = today.AddDays(-1);
                }

                return today;
            }
        }

        private NightFallManager NfManager { get; set; }

        public WeeklyController()
        {
            NfManager = new NightFallManager();
            vendorManager = new VendorManager();
            prisonManager = new PrisonManager();
            bountyManager = new BountyManager();
        }
        
        public ActionResult Index(bool noLayout = false)
        {
            var weekly = NfManager.GetWeekly(StandardDate);
            var raidChallenges = vendorManager.GetRaidChallenges(StandardDate);
            var challengeElders = prisonManager.GetDetailedChallenge(StandardDate);
            var weeklyCrucible = NfManager.GetDetailedCrucibleWeekly(StandardDate);
            var ironBannerBounties = bountyManager.GetBounties(StandardDate, 1, "IronBanner");
            var ironBannerRewards = bountyManager.GetRewards(StandardDate, 1, "IronBanner");

            var model = new WeeklyDataModel()
            {
                ThisDate = StandardDate,
                ThisWeekly = weekly,
                RaidChallenges = raidChallenges,
                EldersChallenge = challengeElders,
                WeeklyCrucible = weeklyCrucible,
                IronBannerBounties = ironBannerBounties,
                IronBannerRewards = ironBannerRewards
            };

            if (noLayout)
                return View("PartialIndex", model);
            else
            {
                ViewBag.HtmlTagOverride = @"data-redirect=""/#weekly""";
                return View("Index", model);
            }
        }
    }
}