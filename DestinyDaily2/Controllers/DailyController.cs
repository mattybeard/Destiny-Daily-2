using System;
using System.Web.Mvc;
using DestinyDaily2.Models;
using DestinyDailyDAL;

namespace DestinyDaily2.Controllers
{
    public class DailyController : Controller
    {
        private DailyManager DailyManager { get; set; }
        private DateTime StandardDate => DateTime.Now.AddHours(-10.0);

        public DailyController()
        {
            DailyManager = new DailyManager();
        }
        public ActionResult Index()
        {
            var daily = DailyManager.GetDaily(StandardDate);
            var dailyStructure = new HeroicDailyModel() {DailyMission = daily};
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

            dailyStructure.DisplayDate = StandardDate;
            return View(dailyStructure);
        }
    }
} 