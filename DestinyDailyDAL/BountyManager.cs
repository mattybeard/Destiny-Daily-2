using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinyDailyApiManager.Models;

namespace DestinyDailyDAL
{
    public class BountyManager
    {
        private DestinySqlEntities db { get; }

        private DateTime ThisDate => DateTime.Now.AddHours(-9.0).AddMinutes(2);

        private DateTime ThisWeeklyDate
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

        public BountyManager()
        {
            db = new DestinySqlEntities();
        }
        public List<BountyDay> GetBounties(DateTime date, int tryCount)
        {
            var bounties = db.BountyDays.Where(d => d.year == date.Year && d.month == date.Month && d.day == date.Day).ToList();
            if (!bounties.Any() && tryCount == 1)
                CreateBounties(date);

            return bounties;
        }

        public List<BountyDay> GetBounties(DateTime date, int tryCount, string type)
        {
            var bounties = db.BountyDays.Where(d => d.year == date.Year && d.month == date.Month && d.day == date.Day).ToList();
          
            return bounties.Where(c => c.vendor == type).ToList();
        }

        public List<RewardsDay> GetRewards(DateTime date, int tryCount, string type)
        {
            var rewards = db.RewardsDays.Where(d => d.year == date.Year && d.month == date.Month && d.day == date.Day).ToList();

            return rewards.Where(c => c.vendor == type).ToList();
        }

        public void CreateBounties(DateTime date, Vendors vendor = Vendors.All)
        {
            var vendorInformation = DestinyDailyApiManager.BungieApi.GetAdvisors();
            if (vendorInformation.ErrorCode > 1)
                return;

            if (date.AddHours(9) < vendorInformation.Response.data.activities.dailychapter.status.startDate)
                return;

            if (vendor == Vendors.All || vendor == Vendors.Vanguard || vendor == Vendors.Crucible)
            {
                var oldEndpoint = DestinyDailyApiManager.BungieApi.GetVendorMetaData("1527174714");
                if (oldEndpoint != null && oldEndpoint.ErrorCode <= 1)
                {
                    var bountiesCategory = oldEndpoint.Response.data.vendor.saleItemCategories.First(c => c.categoryTitle == "Available Bounties");
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

            if ((vendor == Vendors.All || vendor == Vendors.Variks ) && IsResetDate(date))
            {
                var bounties = vendorInformation.Response.data.activities.elderchallenge.bountyHashes.ToArray();
                CreateVendorBounties(bounties, date, Vendors.Variks);
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
                        CreateVendorBounties(bounties, date, Vendors.Zavala);
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
                        CreateVendorBounties(bounties, date, Vendors.IronBanner);
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
                if (oldEndpoint != null)
                {
                    var bountiesCategory = oldEndpoint.Response.data.vendor.saleItemCategories.FirstOrDefault(c => c.categoryTitle == "Iron Lord Bounties");

                    if (bountiesCategory != null)
                    {
                        var bounties = bountiesCategory.saleItems.Select(c => c.item.itemHash).ToArray();
                        CreateVendorBounties(bounties, date, Vendors.Shiro);
                    }
                }

                oldEndpoint = DestinyDailyApiManager.BungieApi.GetVendorMetaData("2190824863");
                if (oldEndpoint != null)
                {
                    var artifactCategory = oldEndpoint.Response.data.vendor.saleItemCategories.FirstOrDefault(c => c.categoryTitle == "Iron Lord Artifacts");

                    if (artifactCategory != null)
                    {
                        var items = artifactCategory.saleItems.Select(c => c.item.itemHash).ToArray();
                        CreateVendorRewards(items, date, Vendors.Tyra);
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
            var ibBounties = GetBounties(ThisWeeklyDate, 1, "IronBanner");

            return ibBounties.Any();
        }
    }
}
