using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DestinyDaily2.Models;
using DestinyDailyDAL;

namespace DestinyDaily2.Controllers
{
    public class XurController : Controller
    {
        private DateTime StandardDate
        {
            get
            {
                var today = DateTime.Now.AddHours(-10.0);
                while (today.DayOfWeek != DayOfWeek.Friday)
                {
                    today = today.AddDays(-1);
                }

                return today;
            }
        }

        private bool XurInTower
        {
            get
            {
                var timeZone = DateTime.Now.AddHours(-10.0);
                if (timeZone.DayOfWeek == DayOfWeek.Friday || timeZone.DayOfWeek == DayOfWeek.Saturday)
                    return true;

                return false;
            }
        }
        
        private XurManager XurManager { get; set; }

        public XurController()
        {
            XurManager = new XurManager();
        }

        public ActionResult Index()
        {
            if (!XurInTower)
                return View("NotHere");

            var xurItems = XurManager.GetItems(StandardDate);
                return View(xurItems);
        }
    }
}