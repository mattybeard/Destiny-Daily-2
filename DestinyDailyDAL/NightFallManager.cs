using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinyDailyApiManager.Models.D2.API;
using DestinyDailyApiManager.Models.D2.Groups;
using DestinyDailyDAL.Destiny1;
using DestinyDailyDAL.Destiny2;
using DestinyDailyDAL.Destiny2.Models;
using DestinyDailyDAL.Models;

namespace DestinyDailyDAL
{
    public class NightFallManager : DestinyDailyManager
    {
        private DestinySqlEntities db { get; set; }
        private DestinyDaily2Entities db2 { get; }

        public NightFallManager()
        {
            db = new DestinySqlEntities();
            db2 = new DestinyDaily2Entities();
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

        public NightfallManifestModel GetD2NightFall()
        {
            return GetD2NightFall(WeeklyDate);
        }

        public NightfallManifestModel GetD2NightFall(DateTime date)
        {
           NightfallManifestGroupModel nightfallManifest = null;
            var nightfall = db2.D2Nightfalls.FirstOrDefault(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
            if (nightfall == null)
            {
                nightfallManifest = CreateD2NightFall(date, true);
                nightfall = db2.D2Nightfalls.FirstOrDefault(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
            }

            if (nightfall != null)
            {
                if (nightfallManifest == null)
                    nightfallManifest = CreateD2NightFall(date, false);

                var model = new NightfallManifestModel
                {
                    Hash = nightfall.missionid,
                    Name = nightfallManifest.Activity.displayProperties.name,
                    PgcrIcon = nightfallManifest.Activity.pgcrImage,
                    Modifiers = nightfallManifest.Modifiers.Select(m => new UnclassifiedModifier() { Name = m.displayProperties.name, Description = m.displayProperties.description, Icon = m.displayProperties.icon }).ToList(),
                    Challenges = nightfallManifest.Challenges.Select(m => new UnclassifiedChallenge() {Name =  m.displayProperties.name, Description = m.displayProperties.description}).ToList(),
                };

                return model;
            }

            return null;
        }

        private NightfallManifestGroupModel CreateD2NightFall(DateTime date, bool createNew)
        {
            var nightfallDefinition = new NightfallManifestGroupModel();

            var mileStones = DestinyDailyApiManager.BungieApi.GetMilestones();
            if (mileStones.ErrorCode > 1)
                return null;

            var activityManifest = DestinyDailyApiManager.BungieApi.GetPlumbing<Dictionary<long, ActivityDefinition>>("DestinyActivityDefinition");
            var modifierManifest = DestinyDailyApiManager.BungieApi.GetPlumbing<Dictionary<long, ModifierDefinition>>("DestinyActivityModifierDefinition");
            var objectiveManifest = DestinyDailyApiManager.BungieApi.GetPlumbing<Dictionary<long, ModifierDefinition>>("DestinyObjectiveDefinition");
            var vendorManifest = DestinyDailyApiManager.BungieApi.GetPlumbing<Dictionary<long, VendorDefinition>>("DestinyVendorDefinition");

            foreach (var milestone in mileStones.Response)
            {                
                var milestoneDetails = activityManifest[milestone.Value.availableQuests[0].activity.activityHash];
                if (milestoneDetails.displayProperties.name.Contains("Nightfall"))
                {
                    if (createNew)
                    {
                        var newEntry = new D2Nightfall()
                        {
                            missionid = milestoneDetails.hash,
                            day = date.Day,
                            month = date.Month,
                            year = date.Year
                        };
                        db2.D2Nightfalls.Add(newEntry);
                        db2.SaveChanges();
                    }

                    nightfallDefinition.Activity = milestoneDetails;

                    foreach (var modifier in milestone.Value.availableQuests[0].activity.modifierHashes)
                    {
                        if (createNew)
                        {
                            var modifierEntry = new D2NightfallModifier()
                            {
                                modfierid = modifier,
                                day = date.Day,
                                month = date.Month,
                                year = date.Year
                            };
                            db2.D2NightfallModifiers.Add(modifierEntry);
                            db2.SaveChanges();
                        }

                        var classifiedOverride = db2.ClassifiedOverrides.FirstOrDefault(c => c.id == modifier);
                        if (classifiedOverride != null)
                        {
                            var overrideManifest = new ModifierDefinition()
                            {
                                hash = modifier,
                                displayProperties = new DisplayProperties()
                                {
                                    name = classifiedOverride.name,
                                    description = classifiedOverride.desc
                                }
                            };
                            nightfallDefinition.Modifiers.Add(overrideManifest);
                        }
                        else 
                            nightfallDefinition.Modifiers.Add(modifierManifest[modifier]);
                    }

                    var tiers = milestone.Value.availableQuests[0].challenges.Select(c => c.activityHash).First();
                    foreach (var challenge in milestone.Value.availableQuests[0].challenges.Where(c => c.activityHash == tiers))
                    {

                        if (createNew)
                        {
                            var challengeEntry = new D2NightfallChallenge()
                            {
                                objectiveid = challenge.objectiveHash,
                                day = date.Day,
                                month = date.Month,
                                year = date.Year
                            };
                            db2.D2NightfallChallenges.Add(challengeEntry);
                            db2.SaveChanges();
                        }

                        nightfallDefinition.Challenges.Add(objectiveManifest[challenge.objectiveHash]);
                    }

                    return nightfallDefinition;
                }
            }
            return null;
        }

        private ActivityDefinition GetActivityDefinition(long? hash)
        {
            if (hash == null)
                return null;

            var getActivityManifest = DestinyDailyApiManager.BungieApi.GetPlumbing<Dictionary<long, ActivityDefinition>>("DestinyActivityDefinition");
            return getActivityManifest[hash.Value];
        }

        public Destiny1.Nightfall GetD1NightFall()
        {
            return GetD1NightFall(WeeklyDate);
        }

        public DestinyDailyDAL.Destiny1.Nightfall GetD1NightFall(DateTime date)
        {
            var nightfall = db.Nightfalls.FirstOrDefault(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
            if (nightfall == null)
            {
                CreateD1NightFall(date);
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

        private void CreateD1NightFall(DateTime date)
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
                var newEntry = new Destiny1.Nightfall()
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
