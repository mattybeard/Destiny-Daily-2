using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DestinyDailyDAL;
using DestinyDailyDAL.Models;

namespace DestinyDaily2.Models
{
    public class WeeklyDataModel
    {
        public DateTime ThisDate { get; set; }
        public WeeklyHeroic ThisWeekly { get; set; }
        public List<BountyDay> StrikeBounties { get; set; }
        public List<RaidChallengeDay> RaidChallenges { get; set; }
        public DetailedChallengeOfElders EldersChallenge { get; set; }
        public DetailedWeeklyCrucible WeeklyCrucible { get; set; }
        public List<BountyDay> IronBannerBounties { get; set; }
        public List<RewardsDay> IronBannerRewards { get; set; }
        public DateTime ExpiryTime { get; set; }
        public DateTime StartTime { get; set; }
    }
}