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
        private VendorManager VendorManager { get; }
        private XurManager XurManager { get; }    
        private TrialsManager TrialsManager { get; }
        private BountyManager BountyManager { get; }
        private static VendorContentModel Cache { get; set; }
        private bool CacheExpired
        {
            get
            {
                if (Cache == null)
                    return true;

                if (Cache.ExpiryTime < DateTime.Now)
                    return true;

                if (Cache.ExpiryTime.Hour < DateTime.Now.Hour)
                    return true;

                return false;
            }
        }
        
        public VendorsController()
        {
            VendorManager = new VendorManager();
            XurManager = new XurManager();
            TrialsManager = new TrialsManager();
            BountyManager = new BountyManager();
        }

        public ActionResult Index(bool noLayout = false)
        {
            if (CacheExpired)
            {
                Cache = new VendorContentModel()
                {
                    HideSrl = true,

                    XurInTower = XurManager.InTower,
                    XurSales = XurManager.GetCurrentItems(),
                    XurLocation = XurManager.GetCurrentLocation(),
                    MaterialExchanges = VendorManager.GetMaterialExchange(),
                    MaterialDetail = VendorManager.GetMaterialDetails(),
                    TrialsDetails = TrialsManager.GetCurrentMap(),
                    IronLordBounties = BountyManager.GetWeeklyBounties("Shiro"),
                    IronLordArtifacts = BountyManager.GetWeeklyRewards("Tyra"),
                    SrlBounties = BountyManager.GetBounties("Srl"),
                    SrlRewards = BountyManager.GetRewards(new DateTime(2016,12,13), 1, "Srl"),

                    ExpiryTime = DateTime.Now.AddMinutes(30),
                    StartTime = DateTime.Now
                };
            }

            if (noLayout)
                return View("PartialIndex", Cache);
            else
            {
                ViewBag.HtmlTagOverride = @"data-redirect=""/#vendors""";
                return View("Index", Cache);
            }
        }
    }
}