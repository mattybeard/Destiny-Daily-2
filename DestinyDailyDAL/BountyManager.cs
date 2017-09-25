using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinyDailyApiManager.Models;
using DestinyDailyDAL.Destiny1;
using DestinyDailyDAL.Destiny2;

namespace DestinyDailyDAL
{
    public class BountyManager : DestinyDailyManager
    {
        private DestinySqlEntities db { get; }
        private DestinyDaily2Entities db2 { get; }
        public BountyManager()
        {
            db = new DestinySqlEntities();
            db2 = new DestinyDaily2Entities();
        }

        public void SampleApiTest()
        {
            DestinyDailyApiManager.BungieApi.ApiTest();
        }

        public List<BountyDay> GetBounties()
        {
            return GetBounties(TodayDate, 1);
        }

        public List<BountyDay> GetBounties(DateTime date, int tryCount)
        {
            var bounties = db.BountyDays.Where(d => d.year == date.Year && d.month == date.Month && d.day == date.Day).ToList();
            if (!bounties.Any() && tryCount == 1)
            {
                CreateBounties(date);
                return GetBounties(date, 2);
            }

            if (!bounties.Any() && tryCount == 2)
                return GetBounties(date.AddHours(-2), 2);

            foreach (var bounty in bounties)
            {
                if (bounty.InventoryItem == null)
                    bounty.InventoryItem = db.InventoryItems.FirstOrDefault(i => i.id == bounty.bountyinfoid);
            }

            return bounties;
        }

        public List<BountyDay> GetWeeklyBounties(string type)
        {
            return GetBounties(WeeklyDate, 1, type);
        }

        public List<BountyDay> GetBounties(string type)
        {
            return GetBounties(TodayDate, 1, type);
        }

        public List<BountyDay> GetBounties(DateTime date, int tryCount, string type)
        {
            var bounties = db.BountyDays.Where(d => d.year == date.Year && d.month == date.Month && d.day == date.Day).ToList();
            var typeBounties = bounties.Where(c => c.vendor == type).ToList();

            if (!typeBounties.Any())
            {
                Vendors vendor;
                Enum.TryParse(type, out vendor);

                CreateBounties(date, vendor);
                var updatedBounties = db.BountyDays.Where(d => d.year == date.Year && d.month == date.Month && d.day == date.Day && d.vendor == type).ToList();
                foreach (var bounty in updatedBounties)
                {
                    if(bounty.InventoryItem == null)
                        bounty.InventoryItem = db.InventoryItems.FirstOrDefault(i => i.id == bounty.bountyinfoid);
                }

                return updatedBounties;
            }


            foreach (var bounty in typeBounties)
            {
                if (bounty.InventoryItem == null)
                    bounty.InventoryItem = db.InventoryItems.FirstOrDefault(i => i.id == bounty.bountyinfoid);
            }
            return typeBounties;
        }

        public List<RewardsDay> GetRewards(string type)
        {
            return GetRewards(TodayDate, 1, type);
        }

        public List<RewardsDay> GetWeeklyRewards(string type)
        {
            return GetRewards(WeeklyDate, 1, type);
        }

        public List<RewardsDay> GetRewards(DateTime date, int tryCount, string type)
        {
            var rewards = db.RewardsDays.Where(d => d.year == date.Year && d.month == date.Month && d.day == date.Day).ToList();
            foreach (var reward in rewards)
                reward.inventoryitem = db.InventoryItems.FirstOrDefault(c => c.id == reward.itemid);

            return rewards.Where(c => c.vendor == type).ToList();
        }

        public void CreateBounties(DateTime date, Vendors vendor = Vendors.All)
        {
            var bountyTracker = DestinyDailyApiManager.BungieApi.GetVendorMetaData("1527174714");
            if (bountyTracker.Response.data.vendor.nextRefreshDate < TodayDate)
                return;

            var vendorInformation = DestinyDailyApiManager.BungieApi.GetAdvisors();
            if (vendorInformation.ErrorCode > 1)
                return;

            if (vendor == Vendors.All || vendor == Vendors.Vanguard || vendor == Vendors.Crucible)
            {
                if (bountyTracker != null && bountyTracker.ErrorCode <= 1)
                {
                    var bountiesCategory = bountyTracker.Response.data.vendor.saleItemCategories.First(c => c.categoryTitle == "Available Bounties");
                    var vanguardList = new List<long>();
                    var crucibleList = new List<long>();
                    var strikeList = new List<long>();

                    foreach (var bounty in bountiesCategory.saleItems)
                    {
                        var matchingItem = db.InventoryItems.FirstOrDefault(i => i.id == bounty.item.itemHash);
                        if (matchingItem != null)
                        {
                            switch (matchingItem.type)
                            {
                                case "Vanguard Elite Bounty":
                                    strikeList.Add(matchingItem.id);
                                    break;

                                case "Crucible Bounty":
                                    crucibleList.Add(matchingItem.id);
                                    break;

                                default:
                                    vanguardList.Add(matchingItem.id);
                                    break;
                            }
                        }
                    }
                    CreateVendorBounties(vanguardList.ToArray(), date, Vendors.Vanguard);
                    CreateVendorBounties(crucibleList.ToArray(), date, Vendors.Crucible);
                    CreateVendorBounties(strikeList.ToArray(), date, Vendors.Strike);
                }
            }            

           if (vendor == Vendors.All || vendor == Vendors.Srl)
            {
                var oldEndpoint = DestinyDailyApiManager.BungieApi.GetVendorMetaData("459708109");
                if (oldEndpoint != null && oldEndpoint.ErrorCode <= 1)
                    {
                    var bountiesCategory = oldEndpoint.Response.data.vendor.saleItemCategories.FirstOrDefault(c => c.categoryTitle == "Available Bounties");

                    if (bountiesCategory != null)
                    {
                        var bounties = bountiesCategory.saleItems.Select(c => c.item.itemHash).ToArray();
                        CreateVendorBounties(bounties, date, Vendors.Srl);
                    }
                }
            }

            if ((vendor == Vendors.All || vendor == Vendors.Shaxx) && IsResetDate(date))
            {
                var weeklyBounties = new long[] {1983809889, 2751498155, 1128170519, 3018625105};
                var bounties = vendorInformation.Response.data.activities.weeklycrucible.bountyHashes.Where(bh => weeklyBounties.Contains(bh)).ToArray();
                CreateVendorBounties(bounties, WeeklyDate, Vendors.Shaxx);
            }

            if ((vendor == Vendors.All || vendor == Vendors.Variks ) && IsResetDate(date))
            {
                var bounties = vendorInformation.Response.data.activities.elderchallenge.bountyHashes.ToArray();
                CreateVendorBounties(bounties, WeeklyDate, Vendors.Variks);
            }

            if ((vendor == Vendors.All || vendor == Vendors.Zavala) && IsResetDate(date))
            {
                var oldEndpoint = DestinyDailyApiManager.BungieApi.GetVendorMetaData("1990950");
                if (oldEndpoint != null && oldEndpoint.ErrorCode <= 1)
                    {
                    var bountiesCategory = oldEndpoint.Response.data.vendor.saleItemCategories.FirstOrDefault(c => c.categoryTitle == "Available Bounties");

                    if (bountiesCategory != null)
                    {
                        var bounties = bountiesCategory.saleItems.Select(c => c.item.itemHash).ToArray();
                        CreateVendorBounties(bounties, WeeklyDate, Vendors.Zavala);
                    }
                }
            }

            if ((vendor == Vendors.All || vendor == Vendors.IronBanner) && IsResetDate(date))
            {
                var oldEndpoint = DestinyDailyApiManager.BungieApi.GetVendorMetaData("2610555297");
                if (oldEndpoint != null && oldEndpoint.ErrorCode <= 1)
                {
                    var bountiesCategory = oldEndpoint.Response.data.vendor.saleItemCategories.FirstOrDefault(c => c.categoryTitle == "Available Bounties");

                    if (bountiesCategory != null)
                    {
                        var bounties = bountiesCategory.saleItems.Select(c => c.item.itemHash).ToArray();
                        CreateVendorBounties(bounties, WeeklyDate, Vendors.IronBanner);
                    }

                    var rewardsCategory = oldEndpoint.Response.data.vendor.saleItemCategories.FirstOrDefault(c => c.categoryTitle == "Event Rewards");

                    if (rewardsCategory != null)
                    {
                        var items = rewardsCategory.saleItems.Select(c => c.item.itemHash).ToArray();
                        CreateVendorRewards(items, WeeklyDate, Vendors.IronBanner);
                    }
                }
            }


            if (vendor == Vendors.All || vendor == Vendors.Trials)
            {
                var bounties = vendorInformation.Response.data.activities.trials.bountyHashes.ToArray();
                CreateVendorBounties(bounties, date, Vendors.Trials);
            }

            if ((vendor == Vendors.All || vendor == Vendors.Shiro) && IsResetDate(date))
            {
                // Manually get Shiro from old endpoint
                var oldEndpoint = DestinyDailyApiManager.BungieApi.GetVendorMetaData("2190824860");
                if (oldEndpoint != null && oldEndpoint.Response.data.vendor.nextRefreshDate > DateTime.Now)
                {
                    var bountiesCategory = oldEndpoint.Response.data.vendor.saleItemCategories.FirstOrDefault(c => c.categoryTitle == "Iron Lord Bounties");

                    if (bountiesCategory != null)
                    {
                        var bounties = bountiesCategory.saleItems.Select(c => c.item.itemHash).ToArray();
                        CreateVendorBounties(bounties, WeeklyDate, Vendors.Shiro);
                    }
                }

                oldEndpoint = DestinyDailyApiManager.BungieApi.GetVendorMetaData("2190824863");
                if (oldEndpoint != null && oldEndpoint.Response.data.vendor.nextRefreshDate > DateTime.Now)
                {
                    var artifactCategory = oldEndpoint.Response.data.vendor.saleItemCategories.FirstOrDefault(c => c.categoryTitle == "Iron Lord Artifacts");

                    if (artifactCategory != null)
                    {
                        var items = artifactCategory.saleItems.Select(c => c.item.itemHash).ToArray();
                        CreateVendorRewards(items, WeeklyDate, Vendors.Tyra);
                    }
                }
            }
        }

        private void CreateVendorRewards(long[] hashes, DateTime date, Vendors vendor)
        {
            foreach (var hash in hashes)
            {
                var bountyDay = new RewardsDay()
                {
                    itemid = hash,
                    day = date.Day,
                    month = date.Month,
                    year = date.Year,
                    vendor = vendor.ToString()
                };
                db.RewardsDays.Add(bountyDay);
            }
            db.SaveChanges();
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

        private bool IsResetDate(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Tuesday;
        }

        public IEnumerable<InventoryItem> GetPossibleBounties()
        {
            return db.InventoryItems.Where(i => i.type.Contains("bounty"));
        }

        public bool HasLimitedEventBounties()
        {
            var ibBounties = GetBounties(WeeklyDate, 1, "IronBanner");

            return ibBounties.Any();
        }
    }
}
