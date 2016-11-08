using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DestinyDaily2.Models;
using DestinyDailyApiManager;
using DestinyDailyApiManager.Models;
using DestinyDailyDAL;

namespace DestinyDaily2.Controllers
{
    public class BountyController : Controller
    {
        private BountyManager BountyManager { get; set; }
        private DateTime StandardDate => DateTime.Now.AddHours(-9.0);

        public BountyController()
        {
            BountyManager = new BountyManager();
        }

        public ActionResult Index()
        {
            List<BountyDay> bounties = null;
            bounties = BountyManager.GetBounties(StandardDate, 1);
            if (bounties == null || !bounties.Any())
                bounties = BountyManager.GetBounties(StandardDate, 2);

            ViewBag.VisibleDate = StandardDate.ToString("yyyy-MM-dd");
            return View(bounties);
        }

        public ActionResult Edit()
        {
            var possibleBounties = BountyManager.GetPossibleBounties().OrderBy(i => i.name);

            return View(possibleBounties);
        }

        [HttpPost]
        public ActionResult Edit(BountyEditModel model)
        {
            var db = new DestinySqlEntities();
            if (model.Shaxx1 > 0)
                BountyManager.CreateVendorBounties(new[] { model.Shaxx1, model.Shaxx2, model.Shaxx3, model.Shaxx4, model.Shaxx5, model.Shaxx6 }, StandardDate, Vendors.Shaxx);

            return RedirectToAction("Index");
        }
    }
}