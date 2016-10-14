using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DestinyDaily2.Models;
using DestinyDailyDAL;

namespace DestinyDaily2.Controllers
{
    public class NightfallController : Controller
    {
        private DateTime StandardDate
        {
            get
            {
                var today = DateTime.Now.AddHours(-10.0);
                while (today.DayOfWeek != DayOfWeek.Tuesday)
                {
                    today = today.AddDays(-1);
                }

                return today;
            }
        }

        private NightFallManager NfManager { get; set; }

        public NightfallController()
        {
            NfManager = new NightFallManager();
        }

        public ActionResult Index()
        {
            var nf = NfManager.GetNightFall(StandardDate);
            var weekly = NfManager.GetWeekly(StandardDate);

            var model = new NightfallWeeklyModel()
            {
                ThisDate = StandardDate,
                ThisNightfall = nf,
                ThisWeekly = weekly
            };

            return View(model);
        }
    }
}