using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DestinyDaily2.Models;
using DestinyDailyDAL;

namespace DestinyDaily2.Controllers
{
    public class NightFallController : Controller
    {
        public NightFallManager NfManager { get; set; }

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

        public NightFallController()
        {
            NfManager = new NightFallManager();
        }
        
        public ActionResult Index(bool noLayout = false)
        {
            var nf = NfManager.GetNightFall(StandardDate);
            var weekly = NfManager.GetWeekly(StandardDate);

            var model = new NightfallDataModel()
            {
                ThisDate = StandardDate,
                ThisNightfall = nf
            };

            if (noLayout)
                return View("PartialIndex", model);
            else
            {
                ViewBag.HtmlTagOverride = @"data-redirect=""/#nightfall""";
                return View("Index", model);
            }
        }
    }
}