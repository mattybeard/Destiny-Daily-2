using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinyDailyDAL.Destiny1;

namespace DestinyDailyDAL
{
    public class VendorManager : DestinyDailyManager
    {
        private DestinySqlEntities db { get; set; }

        public VendorManager()
        {
            db = new DestinySqlEntities();
        }

        public List<MaterialExchange> GetMaterialExchange()
        {
            return GetMaterialExchange(WeeklyDate);
        }

        public List<MaterialExchange> GetMaterialExchange(DateTime date)
        {
           var exchanges = db.MaterialExchanges.Where(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
            if (!exchanges.Any())
            {
                CreateMaterialExchanges(date);
                var updatedExchanges = db.MaterialExchanges.Where(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
                return updatedExchanges.ToList();
            }

            return exchanges.ToList();
        }

        private void CreateMaterialExchanges(DateTime standardDate)
        {
            var hiddenHashes = CreateStopHashes();
            var vendorKey = new Dictionary<string, string>
            {
                {"Future War Cult", "1821699360"}, {"Dead Orbit", "3611686524"}, {"New Monarchy", "1808244981"}, {"Eris Morn", "174528503"}
            };

            foreach (var faction in vendorKey)
            {
                var factionDetails = DestinyDailyApiManager.BungieApi.GetVendorMetaData(faction.Value);
                var factionItems = factionDetails.Response.data.vendor.saleItemCategories.FirstOrDefault(c => c.categoryTitle == "Material Exchange" || c.categoryTitle == "Crota's Bane Rank 2" || c.categoryTitle == "Materials Exchange");
                if (factionItems != null)
                {
                    var customExchange = factionItems.saleItems.FirstOrDefault(c => !hiddenHashes.Contains(c.item.itemHash));
                    if (customExchange != null)
                    {
                        var item = db.InventoryItems.FirstOrDefault(c => c.id == customExchange.item.itemHash);
                        if (item != null)
                            CreateMaterialExchange(standardDate, faction.Key, item.name);
                    }
                }
            }

            db.SaveChanges();
        }

        private List<long> CreateStopHashes()
        {
            var blackList = new List<long>();
            blackList.Add(2709988006);
            blackList.Add(1298784603);
            blackList.Add(1590258561);
            blackList.Add(2698915936);
            blackList.Add(279670352);
            blackList.Add(3157586064);
            blackList.Add(3465849241);
            blackList.Add(246318587);
            blackList.Add(2873148414);
            blackList.Add(3069074202);
            blackList.Add(4065259641);
            blackList.Add(3975516730);
            blackList.Add(1989520068);
            blackList.Add(807600833);
            blackList.Add(3722494237);
            return blackList;
        }

        private void CreateMaterialExchange(DateTime standardDate, string vendorName, string name)
        {
            var materialExchange = new MaterialExchange()
            {
                day = standardDate.Day,
                month = standardDate.Month,
                year = standardDate.Year,
                material = name,
                vendor = vendorName
            };

            db.MaterialExchanges.Add(materialExchange);
        }
        public List<RaidChallengeDay> GetRaidChallenges()
        {
            return GetRaidChallenges(WeeklyDate);
        }

        public List<RaidChallengeDay> GetRaidChallenges(DateTime date)
        {
            var challenges = db.RaidChallengeDays.Where(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
            if (!challenges.Any())
            {
                CreateRaidChallenges(date);
                var updatedChallenges = db.RaidChallengeDays.Where(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
                return updatedChallenges.ToList();
            }

            return challenges.ToList();
        }

        private void CreateRaidChallenges(DateTime standardDate)
        {
            foreach (var raid in new string[] { "Kings Fall", "Wrath of the Machine" })
            {
                int newRaidId;
                var lastRaid = db.RaidChallengeDays.Where(c => c.Challenge.raidname == raid).OrderByDescending(a => a.id).FirstOrDefault();
                var possibleRaids = db.RaidChallenges.Where(c => c.raidname == raid).OrderBy(a => a.id).ToList();

                if (lastRaid == null || lastRaid.Challenge.id == possibleRaids.Max(c => c.id))
                    newRaidId = possibleRaids.Min(c => c.id);
                else
                    newRaidId = lastRaid.Challenge.id + 1;

                var newChallenge = new RaidChallengeDay()
                {
                    raidchallengeid = newRaidId,
                    day = standardDate.Day,
                    month = standardDate.Month,
                    year = standardDate.Year
                };

                db.RaidChallengeDays.Add(newChallenge);
                db.SaveChanges();
            }
        }

        public List<InventoryItem> GetMaterialDetails()
        {
            return db.InventoryItems.Where(i => i.type == "material" && (i.name == "Hadium Flake" || i.name == "Helium Filaments" || i.name == "Spinmetal" || i.name == "Spirit Bloom" || i.name == "Relic Iron" || i.name == "Wormspore")).ToList();
        }

        public FeaturedRaidDay GetFeaturedRaid()
        {
            return GetFeaturedRaid(WeeklyDate);
        }

        public FeaturedRaidDay GetFeaturedRaid(DateTime date)
        {
            var featuredRaid = db.FeaturedRaidDays.FirstOrDefault(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
            if (featuredRaid == null)
            {
                CreateFeaturedRaid(date);
                var updatedRaid = db.FeaturedRaidDays.FirstOrDefault(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
                if (updatedRaid != null && updatedRaid.featuredraid == null)
                    updatedRaid.featuredraid = db.FeaturedRaids.FirstOrDefault(fr => fr.id == updatedRaid.featuredraidid);
                return updatedRaid;
            }

            featuredRaid.featuredraid = db.FeaturedRaids.FirstOrDefault(fr => fr.id == featuredRaid.featuredraidid);

            return featuredRaid;
        }

        private void CreateFeaturedRaid(DateTime standardDate)
        {
            int nextRaid;
            var lastRaid = db.FeaturedRaidDays.OrderByDescending(a => a.id).FirstOrDefault();
            if (lastRaid == null)
                nextRaid = 1;
            else if (lastRaid.featuredraidid == 4)
                nextRaid = 1;
            else
                nextRaid = lastRaid.featuredraidid + 1;

            var newFeaturedRaid = new FeaturedRaidDay()
            {
                featuredraidid = nextRaid,
                day = standardDate.Day,
                month = standardDate.Month,
                year = standardDate.Year
            };

            db.FeaturedRaidDays.Add(newFeaturedRaid);
            db.SaveChanges();            
        }
    }
}
