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
        private VendorManager vendorManager { get; }
        private XurManager xurManager { get; }    
        private TrialsManager trialsManager { get; }
        private BountyManager bountyManager { get; }
        private DateTime StandardDate
        {
            get
            {
                var today = DateTime.Now.AddHours(-9.0).AddMinutes(2);
                while (today.DayOfWeek != DayOfWeek.Tuesday)
                {
                    today = today.AddDays(-1);
                }

                return today;
            }
        }

        public VendorsController()
        {
            vendorManager = new VendorManager();
            xurManager = new XurManager();
            trialsManager = new TrialsManager();
            bountyManager = new BountyManager();
        }

        public ActionResult Index(bool noLayout = false)
        {
            var model = new VendorContentModel()
            {
                XurInTower = xurManager.InTower,
                XurSales = xurManager.GetCurrentItems(),
                XurLocation = xurManager.GetCurrentLocation(),
                MaterialExchanges = vendorManager.GetMaterialExchange(StandardDate),
                MaterialDetail = vendorManager.GetMaterialDetails(),
                TrialsDetails = trialsManager.GetCurrentMap(),
                IronLordBounties = bountyManager.GetBounties(StandardDate,1,"Shiro"),
                IronLordArtifacts = bountyManager.GetRewards(StandardDate, 1, "Tyra")
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