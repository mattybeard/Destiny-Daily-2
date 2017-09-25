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
    public class FeaturedEventsController : Controller
    {
        private XurManager XurManager { get; set; }

        public ActionResult Destiny1Index(bool noLayout = false, bool xur = false)
        {
            var model = new FeaturedEventsDataModel();

            if (xur)
                model.XurEvent = XurManager.GetCurrentItems();

            if (noLayout)
                return View("Destiny1/PartialIndex", model);
            else
            {
                //ViewBag.HtmlTagOverride = @"data-redirect=""/#featured""";
                return View("Destiny1/Index", model);
            }
        }
    }
}