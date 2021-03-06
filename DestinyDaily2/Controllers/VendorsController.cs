﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DestinyDaily2.Models;
using DestinyDaily2.Models.Destiny1;
using DestinyDailyDAL;

namespace DestinyDaily2.Controllers
{
    public class VendorsController : Controller
    {
        private VendorManager VendorManager { get; }
        private XurManager XurManager { get; }    
        private TrialsManager TrialsManager { get; }
        private BountyManager BountyManager { get; }
        private static VendorContentModel D1Cache { get; set; }
        private static VendorsModel D2Cache { get; set; }
       
        public VendorsController()
        {
            VendorManager = new VendorManager();
            XurManager = new XurManager();
            TrialsManager = new TrialsManager();
            BountyManager = new BountyManager();
        }

        public ActionResult Index(bool noLayout = false)
        {
            if (D2Cache == null || D2Cache.CacheExpired)
            {
                D2Cache = new VendorsModel()
                {
                    XurInTower = XurManager.InTower(2),
                    XurSales = XurManager.GetD2CurrentItems(),
                    XurLocation = XurManager.GetD2CurrentLocation(),

                    ExpiryTime = DateTime.Now.AddMinutes(30),
                    StartTime = DateTime.Now
                };
            }

            if (noLayout)
                return View("PartialIndex", D2Cache);
            else
            {
                ViewBag.HtmlTagOverride = @"data-redirect=""/#vendors""";
                return View("Index", D2Cache);
            }
        }

        public ActionResult Destiny1Index(bool noLayout = false)
        {
            if (D1Cache ==  null || D1Cache.CacheExpired)
            {
                D1Cache = new VendorContentModel()
                {
                    HideSrl = true,

                    XurInTower = XurManager.InTower(1),
                    XurSales = XurManager.GetD1CurrentItems(),
                    XurLocation = XurManager.GetD1CurrentLocation(),
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
                return View("Destiny1/PartialIndex", D1Cache);
            else
            {
                ViewBag.HtmlTagOverride = @"data-redirect=""/#vendors""";
                return View("Destiny1/Index", D1Cache);
            }
        }
    }
}