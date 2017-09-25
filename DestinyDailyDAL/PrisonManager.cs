using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinyDailyDAL.Destiny1;
using DestinyDailyDAL.Models;

namespace DestinyDailyDAL
{
    public class PrisonManager : DestinyDailyManager
    {
        private DestinySqlEntities db { get; set; }
        private BountyManager bountyManager { get; }
        public PrisonManager()
        {
            db = new DestinySqlEntities();
            bountyManager = new BountyManager();
        }

        public List<PrisonOfElder> GetLegacyPrison()
        {
            return GetLegacyPrison(WeeklyDate);
        }

        public List<PrisonOfElder> GetLegacyPrison(DateTime date)
        {
            var legacyPoE = db.PrisonOfElders.Where(d => d.day == date.Day && d.month == date.Month && d.year == date.Year).ToList();
            if (!legacyPoE.Any())
            {
                CreateLegacyPrison(date);
                var updatedLegacyPoE = db.PrisonOfElders.Where(d => d.day == date.Day && d.month == date.Month && d.year == date.Year).ToList();
                return updatedLegacyPoE;
            }

            return legacyPoE;          
        }

        public DetailedChallengeOfElders GetDetailedChallenge()
        {
            return GetDetailedChallenge(WeeklyDate);
        }

        public DetailedChallengeOfElders GetDetailedChallenge(DateTime date)
        {
            var model = new DetailedChallengeOfElders()
            {
                Challenge = GetChallengePrison(date),
                Bounties = bountyManager.GetWeeklyBounties("Variks")
        };

            return model;
        }

        public ChallengeOfElder GetChallengePrison(DateTime date)
        {
            var newPoE = db.ChallengeOfElders.FirstOrDefault(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
            if (newPoE == null)
            {
                CreateChallengePrison(date);
                var updatedNewPoE = db.ChallengeOfElders.FirstOrDefault(d => d.day == date.Day && d.month == date.Month && d.year == date.Year);
                return updatedNewPoE;
            }

            return newPoE;
        } 

        private void CreateChallengePrison(DateTime weeklyDate)
        {
            var vendorInformation = DestinyDailyApiManager.BungieApi.GetAdvisors();
            if (vendorInformation.ErrorCode > 1)
                return;

            int missionId = 0;
            foreach (var tier in vendorInformation.Response.data.activities.elderchallenge.activityTiers)
            {
                var newMission = new ChallengeOfElder()
                {
                    missionid = tier.activityHash,
                    day = weeklyDate.Day,
                    month = weeklyDate.Month,
                    year = weeklyDate.Year
                };

                db.ChallengeOfElders.Add(newMission);
                db.SaveChanges();
                missionId = newMission.id;

                foreach (var round in tier.extended.rounds)
                {
                    var boss = db.Combatants.FirstOrDefault(b => b.id == round.bossCombatantHash);

                    if (boss != null)
                    {
                        var newBoss = new ChallengeOfEldersRound()
                        {
                            combatantid = boss.id,
                            eldersid = newMission.id
                        };

                        db.ChallengeOfEldersRounds.Add(newBoss);
                    }
                }
                db.SaveChanges();
            }

            foreach (var modifier in vendorInformation.Response.data.activities.elderchallenge.extended.skullCategories.First(s => s.title == "Modifiers").skulls)
            {
                var skull = db.Modifiers.FirstOrDefault(r => r.name == modifier.displayName);
                if (skull != null)
                {
                    var thisSkull = new ChallengeOfEldersModifier()
                    {
                        modifierid = skull.id,
                        challengeofeldersid = missionId
                    };

                    db.ChallengeOfEldersModifiers.Add(thisSkull);
                }
            }

            foreach (var modifier in vendorInformation.Response.data.activities.elderchallenge.extended.skullCategories.First(s => s.title == "Bonuses").skulls)
            {
                var skull = db.ScoreModifiers.FirstOrDefault(r => r.name == modifier.displayName);
                if (skull != null)
                {
                    var thisSkull = new ChallengeOfEldersScoreModifier()
                    {
                        scoremodifierid = skull.id,
                        challengeofeldersid = missionId
                    };

                    db.ChallengeOfEldersScoreModifiers.Add(thisSkull);
                }
            }
            db.SaveChanges();
        }

        private void CreateLegacyPrison(DateTime weeklyDate)
        {
            var vendorInformation = DestinyDailyApiManager.BungieApi.GetAdvisors();
            if (vendorInformation.ErrorCode > 1)
                return;

            foreach (var tier in vendorInformation.Response.data.activities.prisonofelders.activityTiers.OrderBy(c => c.activityData.displayLevel))
            {
                var newMission = new PrisonOfElder()
                {
                    missionid = tier.activityHash,
                    day = weeklyDate.Day,
                    month = weeklyDate.Month,
                    year = weeklyDate.Year
                };

                db.PrisonOfElders.Add(newMission);
                db.SaveChanges();

                var currentRound = 1;
                foreach (var round in tier.extended.rounds)
                {
                    var thisRound = new PrisonOfEldersRound()
                    {
                        prisonofeldersid = newMission.id,
                        round = currentRound++
                    };

                    db.PrisonOfEldersRounds.Add(thisRound);                    

                    var race = db.Races.FirstOrDefault(r => r.manifestId == round.enemyRaceHash);
                    if (race != null)
                    {
                        var thisRace = new PrisonOfEldersRace()
                        {
                            raceid = race.id,
                            levelid = thisRound.id
                        };

                        db.PrisonOfEldersRaces.Add(thisRace);
                    }

                    foreach (var modifier in round.skullCategories[0].skulls)
                    {
                        var skull = db.Modifiers.FirstOrDefault(r => r.name == modifier.displayName);
                        if (skull != null)
                        {
                            var thisSkull = new PrisonOfEldersModifier()
                            {
                                modifierid = skull.id,
                                levelid = thisRound.id
                            };

                            db.PrisonOfEldersModifiers.Add(thisSkull);
                        }
                    }
                    db.SaveChanges();
                }
            }
        }
    }
}
