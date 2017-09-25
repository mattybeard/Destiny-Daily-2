using System;
using System.Collections.Generic;
using DestinyDailyDAL.Destiny1;
using DestinyDailyDAL.Destiny1.Models;

namespace DestinyDaily2.Models.Destiny1
{
    public class HeroicDailyModel
    {
        public HeroicDailyDay DailyMission { get; set; }
        public List<Modifier> DailyModifiers { get; set; }
        public List<ManifestRewardModel> DailyRewards { get; set; }
        public CrucibleDailyDay DailyCrucible { get; set; }
        public List<ManifestRewardModel> DailyCrucibleRewards { get; set; }
        public List<BountyDay> DailyBounties { get; set; }
        public DateTime ExpiryTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime TimeDifferenceTime { get; set; }
        public bool CacheExpired
        {
            get
            {
                if (ExpiryTime < DateTime.Now)
                    return true;

                if (ExpiryTime.Hour < DateTime.Now.Hour)
                    return true;

                return false;
            }
        }
    }
}