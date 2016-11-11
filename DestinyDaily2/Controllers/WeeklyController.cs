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
        private VendorManager vendorManager { get; set; }
        private PrisonManager prisonManager { get; }
        private DateTime TodayDate => DateTime.Now.AddHours(-9.0);

        private DateTime StandardDate
        {
            get
            {
                var today = DateTime.Now.AddHours(-9.0);
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
        }

        public ActionResult Nightfall(bool noLayout = false)
        {
            var nf = NfManager.GetNightFall(StandardDate);
            var weekly = NfManager.GetWeekly(StandardDate);

            var model = new NightfallDataModel()
            {
                ThisDate = StandardDate,
                ThisNightfall = nf
            };

            //ViewBag.HtmlTagOverride = @"data-redirect=""/#nightfall""";

            if (noLayout)
                return View("NightfallIndex", model);
            else
                return View("NightfallDetails", model);
        }

        public ActionResult Weekly(bool noLayout = false)
        {
            var nf = NfManager.GetNightFall(StandardDate);
            var weekly = NfManager.GetWeekly(StandardDate);
            var materialExchanges = vendorManager.GetMaterialExchange(StandardDate);
            var raidChallenges = vendorManager.GetRaidChallenges(StandardDate);
            var challengeElders = prisonManager.GetDetailedChallenge(StandardDate);
            var weeklyCrucible = NfManager.GetDetailedCrucibleWeekly(StandardDate);

            var model = new WeeklyDataModel()
            {
                ThisDate = StandardDate,
                ThisWeekly = weekly,
                RaidChallenges = raidChallenges,
                EldersChallenge = challengeElders,
                WeeklyCrucible = weeklyCrucible
            };

            //ViewBag.HtmlTagOverride = @"data-redirect=""/#nightfall""";

            if (noLayout)
                return View("WeeklyIndex", model);
            else
                return View("WeeklyDetails", model);
        }
    }
}