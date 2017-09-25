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
    public class WeeklyController : Controller
    {
        private VendorManager VendorManager { get; }
        private PrisonManager PrisonManager { get; }
        private BountyManager BountyManager { get; }
        private NightFallManager NfManager { get; set; }
        private static WeeklyDataModel Cache { get; set; }
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

        public WeeklyController()
        {
            NfManager = new NightFallManager();
            VendorManager = new VendorManager();
            PrisonManager = new PrisonManager();
            BountyManager = new BountyManager();
        }
        
        public ActionResult Destiny1Index(bool noLayout = false)
        {
            if (CacheExpired)
            {
                var featuredRaid = VendorManager.GetFeaturedRaid();
                var weekly = NfManager.GetWeekly();
                var weeklyBounties = BountyManager.GetBounties("Strike");
                var raidChallenges = VendorManager.GetRaidChallenges();
                var challengeElders = PrisonManager.GetDetailedChallenge();
                var weeklyCrucible = NfManager.GetDetailedCrucibleWeekly();
                var ironBannerBounties = BountyManager.GetWeeklyBounties("IronBanner");
                var ironBannerRewards = BountyManager.GetWeeklyRewards("IronBanner");
                var weeklyPlaylist = NfManager.GetWeeklyStory();

                Cache = new WeeklyDataModel()
                {
                    ThisWeekly = weekly,
                    StrikeBounties = weeklyBounties,
                    RaidChallenges = raidChallenges,
                    EldersChallenge = challengeElders,
                    WeeklyCrucible = weeklyCrucible,
                    IronBannerBounties = ironBannerBounties,
                    IronBannerRewards = ironBannerRewards,
                    FeaturedRaid = featuredRaid,
                    WeeklyPlaylist = weeklyPlaylist,
                    ExpiryTime = DateTime.Now.AddHours(1),
                    StartTime = DateTime.Now
                };
            }

            if (noLayout)
                return View("Destiny1/PartialIndex", Cache);
            else
            {
                ViewBag.HtmlTagOverride = @"data-redirect=""/#weekly""";
                return View("Destiny1/Index", Cache);
            }
        }

        public ActionResult Index(bool noLayout = false)
        {
            if (noLayout)
                return View("PartialIndex", noLayout);
            else
            {
                ViewBag.HtmlTagOverride = @"data-redirect=""/#weekly""";
                return View("Index", noLayout);
            }
        }
    }
}