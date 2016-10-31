using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinyDailyDAL.Models;

namespace DestinyDailyDAL
{
    public class DailyManager
    {
        private DestinySqlEntities db { get; set; }

        public DailyManager()
        {
            db = new DestinySqlEntities();
        }

        public HeroicDailyDay GetDaily(DateTime standardDate)
        {
            var daily = db.HeroicDailyDays.FirstOrDefault(d => d.day == standardDate.Day && d.month == standardDate.Month && d.year == standardDate.Year);
            if (daily == null)
            {
                CreateDaily(standardDate);
                var updatedDaily = db.HeroicDailyDays.FirstOrDefault(d => d.day == standardDate.Day && d.month == standardDate.Month && d.year == standardDate.Year);
                return updatedDaily;
            }

            return daily;
        }

        private void CreateDaily(DateTime date)
        {
            var vendorInformation = DestinyDailyApiManager.BungieApi.GetAdvisors();
            if (vendorInformation.ErrorCode > 1)
                return;

            var dailyHash = vendorInformation.Response.data.activities.dailychapter.activityTiers[0].activityHash;
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

        public CrucibleDailyDay GetDailyCrucible(DateTime standardDate)
        {
            var daily = db.CrucibleDailyDays.FirstOrDefault(d => d.day == standardDate.Day && d.month == standardDate.Month && d.year == standardDate.Year);
            if (daily == null)
            {
                CreateDailyCrucible(standardDate);
                var updatedDaily = db.CrucibleDailyDays.FirstOrDefault(d => d.day == standardDate.Day && d.month == standardDate.Month && d.year == standardDate.Year);
                return updatedDaily;
            }

            return daily;
        }


        public void CreateDailyCrucible(DateTime date)
        {
            var vendorInformation = DestinyDailyApiManager.BungieApi.GetAdvisors();
            if (vendorInformation.ErrorCode > 1)
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
