using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DestinyDaily2.Models;
using DestinyDailyDAL;

namespace DestinyDaily2.Controllers
{
    public class OtherController : Controller
    {
        private VendorManager vendorManager { get; set; }
        private DateTime TodayDate => DateTime.Now.AddHours(-10.0);

        private DateTime WeeklyDate
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

        public OtherController()
        {
            vendorManager = new VendorManager();
        }
        public ActionResult Index()
        {
            var model = new MiscModel()
            {
                MaterialExchanges = vendorManager.GetMaterialExchange(WeeklyDate)
            };

            return View(model);
        }
    }
}