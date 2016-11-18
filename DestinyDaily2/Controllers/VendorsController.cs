using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DestinyDaily2.Models;
using DestinyDailyDAL;

namespace DestinyDaily2.Controllers
{
    public class VendorsController : Controller
    {
        private VendorManager vendorManager { get; set; }
        private XurManager xurManager { get; set; }

        public VendorsController()
        {
            vendorManager = new VendorManager();
            xurManager = new XurManager();
        }

        public ActionResult Index(bool noLayout = false)
        {
            var model = new VendorContentModel()
            {
                XurInTower = xurManager.InTower,
                XurSales = xurManager.GetCurrentItems(),
                XurLocation = xurManager.GetCurrentLocation()
            };

            if (noLayout)
                return View("PartialIndex", model);
            else
            {
                ViewBag.HtmlTagOverride = @"data-redirect=""/#vendors""";
                return View("Index", model);
            }
        }
    }
}