using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DestinyDailyDAL;

namespace DestinyDaily2.Controllers
{
    public class PrisonController : Controller
    {
        private PrisonManager PrisonManager { get; set; }
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

        public PrisonController()
        {
            PrisonManager = new PrisonManager();
        }

        public ActionResult Index()
        {
            var model = new PrisonModel()
            {
                Legacy = PrisonManager.GetLegacyPrison(StandardDate).ToList(),
                Challenge = PrisonManager.GetChallengePrison(StandardDate)
            };

            var rounds = model.Challenge.Rounds.ToList();
            var combatents = rounds.Select(c => c.Combatant).ToList();

            return View(model);
        }
    }

    public class PrisonModel
    {
        public List<PrisonOfElder> Legacy { get; set; }
        public ChallengeOfElder Challenge { get; set; }
    }
}