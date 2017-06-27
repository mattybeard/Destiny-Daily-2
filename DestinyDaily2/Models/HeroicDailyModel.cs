using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DestinyDailyDAL;
using DestinyDailyDAL.Models;

namespace DestinyDaily2.Models
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
    }
}