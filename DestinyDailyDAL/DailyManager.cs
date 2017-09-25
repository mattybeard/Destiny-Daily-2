using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinyDailyDAL.Destiny1;
using DestinyDailyDAL.Destiny1.Models;
using DestinyDailyDAL.Destiny2;
using DestinyDailyDAL.Models;

namespace DestinyDailyDAL
{
    public class DailyManager : DestinyDailyManager
    {
        private DestinySqlEntities db { get; set; }

        public DailyManager()
        {
            db = new DestinySqlEntities();
        }

        public HeroicDailyDay GetDaily()
        {
            return GetDaily(TodayDate);
        }

        public HeroicDailyDay GetDaily(DateTime date)
        {
            var daily = db.HeroicDailyDays.FirstOrDefault(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
            if (daily == null)
            {
                CreateDaily(date);
                var updatedDaily = db.HeroicDailyDays.FirstOrDefault(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
                return updatedDaily;
            }

            return daily;
        }

        private void CreateDaily(DateTime date)
        {
            var vendorInformation = DestinyDailyApiManager.BungieApi.GetAdvisors();
            if (vendorInformation.ErrorCode > 1)
                return;

            if (vendorInformation.Response.data.activities.dailychapter == null)
                return;

            var previousDailyDate = date.AddDays(-1);
            var previousDaily = db.HeroicDailyDays.FirstOrDefault(d => d.day == previousDailyDate.Day && d.month == previousDailyDate.Month && d.year == previousDailyDate.Year);
            var dailyHash = vendorInformation.Response.data.activities.dailychapter.activityTiers[0].activityHash;

            // Make sure it's different, otherwise play ignore.
            if (previousDaily != null && previousDaily.missionid == dailyHash)
                return;

            var activity = db.ManifestActivities.FirstOrDefault(m => m.id == dailyHash);
            if (activity != null)
            {
                var newEntry = new HeroicDailyDay()
                {
                    missionid = dailyHash,
                    day = date.Day,
                    month = date.Month,
                    year = date.Year
                };
                db.HeroicDailyDays.Add(newEntry);
                db.SaveChanges();
            }
        }

        public List<Modifier> GetModifiers(long? missionId)
        {
            var mission = db.ManifestActivities.FirstOrDefault(d => d.id == missionId);
            return mission?.ManifestSkulls.Select(modifer => db.Modifiers.FirstOrDefault(m => m.name == modifer.name)).Where(matchingMod => matchingMod != null).ToList();
        }

        public List<ManifestRewardModel> GetRewards(long? missionId)
        {
            var mission = db.ManifestActivities.FirstOrDefault(d => d.id == missionId);
            if (mission == null)
                return null;
            
            var manifestRewards = new List<ManifestRewardModel>();
            foreach (var potentialReward in mission.ManifestRewards)
            {
                var matchingItem = db.InventoryItems.FirstOrDefault(i => i.id == potentialReward.rewardHash);
                if (matchingItem != null)
                {
                    var manifestReward = new ManifestRewardModel()
                    {
                        Item = matchingItem,
                        Quantity = potentialReward.value
                    };
                    manifestRewards.Add(manifestReward);
                }
            }
            
            return manifestRewards;
        }

        public CrucibleDailyDay GetDailyCrucible()
        {
            return GetDailyCrucible(TodayDate);
        }

        public CrucibleDailyDay GetDailyCrucible(DateTime date)
        {
            var daily = db.CrucibleDailyDays.FirstOrDefault(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
            if (daily == null)
            {
                CreateDailyCrucible(date);
                var updatedDaily = db.CrucibleDailyDays.FirstOrDefault(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
                return updatedDaily;
            }

            return daily;
        }


        public void CreateDailyCrucible(DateTime date)
        {
            var vendorInformation = DestinyDailyApiManager.BungieApi.GetAdvisors();
            if (vendorInformation.ErrorCode > 1)
                return;

            if (date.AddHours(9) > vendorInformation.Response.data.activities.dailycrucible.status.expirationDate.Date.AddDays(-1))
                return;

            var dailyHash = vendorInformation.Response.data.activities.dailycrucible.activityTiers[0].activityHash;
            var activity = db.ManifestActivities.FirstOrDefault(m => m.id == dailyHash);

            if (activity != null)
            {
                var newEntry = new CrucibleDailyDay()
                {
                    activityid = dailyHash,
                    day = date.Day,
                    month = date.Month,
                    year = date.Year
                };
                db.CrucibleDailyDays.Add(newEntry);
                db.SaveChanges();
            }
        }
    }
}
