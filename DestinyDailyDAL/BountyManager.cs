﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinyDailyApiManager.Models;

namespace DestinyDailyDAL
{
    public class BountyManager
    {
        private DestinyEntities db { get; set; }

        public BountyManager()
        {
            db = new DestinyEntities();
        }
        public List<BountyDay> GetBounties(DateTime date, int tryCount)
        {
            var bounties = db.BountyDays.Where(d => d.year == date.Year && d.month == date.Month && d.day == date.Day).ToList();
            if (!bounties.Any() && tryCount == 1)
                CreateBounties(date);

            return bounties;
        }
        
        public void CreateBounties(DateTime date, Vendors vendor = Vendors.All)
        {
            var vendorInformation = DestinyDailyApiManager.BungieApi.GetAdvisors();
            if (vendorInformation.ErrorCode > 1)
                return;

            if (vendor == Vendors.All || vendor == Vendors.Vanguard)
            {
                var bounties = vendorInformation.Response.data.activities.dailychapter.bountyHashes.ToArray();
                CreateVendorBounties(bounties, date, Vendors.Vanguard);
            }

            if (vendor == Vendors.All || vendor == Vendors.Crucible)
            {
                var bounties = vendorInformation.Response.data.activities.dailycrucible.bountyHashes.ToArray();
                CreateVendorBounties(bounties, date, Vendors.Crucible);
            }

            //TODO: Try Iron Banner BUT from the old Vendor endpoint as not currently exposed.
        }

        public void CreateVendorBounties(long[] hashes, DateTime date, Vendors vendor)
        {
            foreach (var hash in hashes)
            {
                var bountyDay = new BountyDay()
                {
                    bountyinfoid = hash,
                    day = date.Day,
                    month = date.Month,
                    year = date.Year,
                    vendor = vendor.ToString()
                };
                db.BountyDays.Add(bountyDay);
            }
            db.SaveChanges();
        }

        public IEnumerable<InventoryItem> GetPossibleBounties()
        {
            return db.InventoryItems.Where(i => i.type.Contains("bounty"));
        }
    }
}
