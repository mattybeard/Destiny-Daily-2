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
        private static WeeklyDataModel cache { get; set; }
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
            if (CacheExpired)
            {
                var featuredRaid = vendorManager.GetFeaturedRaid(StandardDate);
                var weekly = NfManager.GetWeekly(StandardDate);
                var weeklyBounties = bountyManager.GetBounties(TodayDate, 1, "Strike");
                var raidChallenges = vendorManager.GetRaidChallenges(StandardDate);
                var challengeElders = prisonManager.GetDetailedChallenge(StandardDate);
                var weeklyCrucible = NfManager.GetDetailedCrucibleWeekly(StandardDate);
                var ironBannerBounties = bountyManager.GetBounties(StandardDate, 1, "IronBanner");
                var ironBannerRewards = bountyManager.GetRewards(StandardDate, 1, "IronBanner");

                cache = new WeeklyDataModel()
                {
                    ThisDate = StandardDate,
                    ThisWeekly = weekly,
                    StrikeBounties = weeklyBounties,
                    RaidChallenges = raidChallenges,
                    EldersChallenge = challengeElders,
                    WeeklyCrucible = weeklyCrucible,
                    IronBannerBounties = ironBannerBounties,
                    IronBannerRewards = ironBannerRewards,
                    FeaturedRaid = featuredRaid,
                    ExpiryTime = DateTime.Now.AddHours(1),
                    StartTime = DateTime.Now
                };
            }

            if (noLayout)
                return View("PartialIndex", cache);
            else
            {
                ViewBag.HtmlTagOverride = @"data-redirect=""/#weekly""";
                return View("Index", cache);
            }
        }
    }
}