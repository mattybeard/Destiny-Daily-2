using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinyDailyDAL.Models;

namespace DestinyDailyDAL
{
    public class NightFallManager : DestinyDailyManager
    {
        private DestinySqlEntities db { get; set; }

        public NightFallManager()
        {
            db = new DestinySqlEntities();
        }

        public DetailedWeeklyCrucible GetDetailedCrucibleWeekly()
        {
            return GetDetailedCrucibleWeekly(WeeklyDate);
        }

        public DetailedWeeklyCrucible GetDetailedCrucibleWeekly(DateTime date)
        {
            var model = new DetailedWeeklyCrucible()
            {
                Weekly = GetCrucibleWeekly(date),
                Bounties = new BountyManager().GetBounties(date, 1, "Shaxx")
            };

            return model;
        }

        public CrucibleWeeklyDay GetCrucibleWeekly()
        {
            return GetCrucibleWeekly(WeeklyDate);
        }

        private CrucibleWeeklyDay GetCrucibleWeekly(DateTime date)
        {
            var weekly = db.CrucibleWeeklyDays.FirstOrDefault(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
            if (weekly == null)
            {
                CreateWeeklyCrucible(date);
                return GetCrucibleWeekly(date);
            }
            weekly.ManifestActivity = db.ManifestActivities.FirstOrDefault(c => c.id == weekly.activityid);
            return weekly;
        }

        public Nightfall GetNightFall()
        {
            return GetNightFall(WeeklyDate);
        }

        public Nightfall GetNightFall(DateTime date)
        {
            var nightfall = db.Nightfalls.FirstOrDefault(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
            if (nightfall == null)
            {
                CreateNightFall(date);
                var updatedNightfall = db.Nightfalls.FirstOrDefault(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
                if(updatedNightfall != null)
                    updatedNightfall.ManifestActivity = db.ManifestActivities.FirstOrDefault(m => m.id == updatedNightfall.missionid);

                return updatedNightfall;
            }

            nightfall.ManifestActivity = db.ManifestActivities.FirstOrDefault(m => m.id == nightfall.missionid);
            return nightfall;
        }

        public WeeklyHeroic GetWeekly()
        {
            return GetWeekly(WeeklyDate);
        }

        public WeeklyHeroic GetWeekly(DateTime date)
        {
            var weekly = db.WeeklyHeroics.FirstOrDefault(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
            if (weekly == null)
            {
                CreateWeekly(date);
                var updatedWeekly = db.WeeklyHeroics.FirstOrDefault(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
                return updatedWeekly;
            }

            return weekly;
        }

        private void CreateNightFall(DateTime date)
        {
            var vendorInformation = DestinyDailyApiManager.BungieApi.GetAdvisors();
            if (vendorInformation.ErrorCode > 1)
                return;

            var previousDate = date.AddDays(-7);
            var previous = db.Nightfalls.FirstOrDefault(d => d.day == previousDate.Day && d.month == previousDate.Month && d.year == previousDate.Year);
            var activityHash = vendorInformation.Response.data.activities.nightfall.activityTiers[0].activityHash;

            // Make sure it's different, otherwise play ignore.
            if (previous != null && previous.missionid == activityHash)
                return;

            var activity = db.ManifestActivities.FirstOrDefault(m => m.id == activityHash);
            if (activity != null)
            {
                var newEntry = new Nightfall()
                {
                    missionid = activityHash,
                    day = date.Day,
                    month = date.Month,
                    year = date.Year
                };
                db.Nightfalls.Add(newEntry);
                db.SaveChanges();

                foreach (var skull in vendorInformation.Response.data.activities.nightfall.extended.skullCategories.First(m => m.title == "Modifiers").skulls)
                {
                    var matchingMod = db.Modifiers.FirstOrDefault(m => m.name == skull.displayName);
                    if (matchingMod != null)
                    {
                        var newMod = new NightfallMissionModifier()
                        {
                            modifierid = matchingMod.id,
                            nightfallmissionid = newEntry.id
                        };

                        db.NightfallMissionModifiers.Add(newMod);
                    }
                    db.SaveChanges();
                }

                foreach (var rewardGroup in vendorInformation.Response.data.activities.nightfall.activityTiers[0].rewards)
                {
                    foreach (var reward in rewardGroup.rewardItems)
                    {
                        var matchingReward = db.InventoryItems.FirstOrDefault(r => r.id == reward.itemHash);
                        if (matchingReward != null)
                        {
                            var newReward = new NightfallMissionReward()
                            {
                                rewardid = reward.itemHash,
                                nightfallmissionid = newEntry.id
                            };

                            db.NightfallMissionRewards.Add(newReward);
                        }
                    }
                    db.SaveChanges();
                }
            }
        }

        private void CreateWeeklyCrucible(DateTime date)
        {
            var vendorInformation = DestinyDailyApiManager.BungieApi.GetAdvisors();
            if (vendorInformation.ErrorCode > 1)
                return;

            var activityHash = vendorInformation.Response.data.activities.weeklycrucible.activityTiers[0].activityHash;
            var activity = db.ManifestActivities.FirstOrDefault(m => m.id == activityHash);
            if (activity != null)
            {
                var newEntry = new CrucibleWeeklyDay()
                {
                    activityid = activityHash,
                    day = date.Day,
                    month = date.Month,
                    year = date.Year
                };
                db.CrucibleWeeklyDays.Add(newEntry);
                db.SaveChanges();
            }
        }

        public void CreateWeekly(DateTime date)
        {
            var vendorInformation = DestinyDailyApiManager.BungieApi.GetAdvisors();
            if (vendorInformation.ErrorCode > 1)
                return;

            if (date.AddHours(9) < vendorInformation.Response.data.activities.heroicstrike.status.startDate)
                return;

            var activityHash = vendorInformation.Response.data.activities.heroicstrike.activityTiers[0].activityHash;
            var activity = db.ManifestActivities.FirstOrDefault(m => m.id == activityHash);
            if (activity != null)
            {
                var newEntry = new WeeklyHeroic()
                {
                    missionid = activityHash,
                    day = date.Day,
                    month = date.Month,
                    year = date.Year
                };
                db.WeeklyHeroics.Add(newEntry);
                db.SaveChanges();

                foreach (var skull in vendorInformation.Response.data.activities.heroicstrike.extended.skullCategories.First(m => m.title == "Modifiers").skulls)
                {
                    var matchingMod = db.Modifiers.FirstOrDefault(m => m.name == skull.displayName);
                    if (matchingMod != null)
                    {
                        var newMod = new WeeklyHeroicModifier()
                        {
                            modifierid = matchingMod.id,
                            weeklyheroicid = newEntry.id
                        };

                        db.WeeklyHeroicModifiers.Add(newMod);
                    }
                    db.SaveChanges();
                }

                foreach (var rewardGroup in vendorInformation.Response.data.activities.heroicstrike.activityTiers[0].rewards)
                {
                    foreach (var reward in rewardGroup.rewardItems)
                    {
                        var matchingReward = db.InventoryItems.FirstOrDefault(r => r.id == reward.itemHash);
                        if (matchingReward != null)
                        {
                            var newReward = new WeeklyHeroicMissionReward()
                            {
                                rewardid = reward.itemHash,
                                weeklyheroicid = newEntry.id
                            };

                            db.WeeklyHeroicMissionRewards.Add(newReward);
                        }
                    }
                    db.SaveChanges();
                }
            }
        }

        public WeeklyFeatured GetWeeklyStory()
        {
            return GetWeeklyStory(WeeklyDate);
        }

        public WeeklyFeatured GetWeeklyStory(DateTime date)
        {
            var weekly = db.WeeklyFeatureds.FirstOrDefault(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
            if (weekly == null)
            {
                CreateWeeklyFeatured(date);
                var updatedWeekly = db.WeeklyFeatureds.FirstOrDefault(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);

                if(updatedWeekly != null)
                    updatedWeekly.manifestactivity = db.ManifestActivities.FirstOrDefault(m => m.id == updatedWeekly.playlistid);

                return updatedWeekly;
            }

            weekly.manifestactivity = db.ManifestActivities.FirstOrDefault(m => m.id == weekly.playlistid);

            return weekly;
        }

        private void CreateWeeklyFeatured(DateTime date)
        {
            var vendorInformation = DestinyDailyApiManager.BungieApi.GetAdvisors();
            if (vendorInformation.ErrorCode > 1)
                return;

            var activityHash = vendorInformation.Response.data.activities.weeklystory.activityTiers[0].activityHash;
            var activity = db.ManifestActivities.FirstOrDefault(m => m.id == activityHash);
            if (activity != null)
            {
                var newEntry = new WeeklyFeatured()
                {
                    playlistid = activityHash,
                    day = date.Day,
                    month = date.Month,
                    year = date.Year
                };
                db.WeeklyFeatureds.Add(newEntry);
                db.SaveChanges();

                foreach (var skull in vendorInformation.Response.data.activities.weeklystory.extended.skullCategories.First(m => m.title == "Modifiers").skulls)
                {
                    var matchingMod = db.Modifiers.FirstOrDefault(m => m.name == skull.displayName);
                    if (matchingMod != null)
                    {
                        var newMod = new WeeklyFeaturedModifier()
                        {
                            modifierid = matchingMod.id,
                            weeklyfeaturedid = newEntry.id
                        };

                        db.WeeklyFeaturedModifiers.Add(newMod);
                    }
                    db.SaveChanges();
                }

                foreach (var rewardGroup in vendorInformation.Response.data.activities.weeklystory.activityTiers[0].rewards)
                {
                    foreach (var reward in rewardGroup.rewardItems)
                    {
                        var matchingReward = db.InventoryItems.FirstOrDefault(r => r.id == reward.itemHash);
                        if (matchingReward != null)
                        {
                            var newReward = new WeeklyFeaturedReward()
                            {
                                rewardid = reward.itemHash,
                                weeklyfeaturedid = newEntry.id
                            };

                            db.WeeklyFeaturedRewards.Add(newReward);
                        }
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}
