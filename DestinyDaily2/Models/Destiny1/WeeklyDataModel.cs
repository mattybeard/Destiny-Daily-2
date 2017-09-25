using System;
using System.Collections.Generic;
using DestinyDailyDAL.Destiny1;
using DestinyDailyDAL.Models;

namespace DestinyDaily2.Models.Destiny1
{
    public class WeeklyDataModel
    {
        public WeeklyHeroic ThisWeekly { get; set; }
        public List<BountyDay> StrikeBounties { get; set; }
        public FeaturedRaidDay FeaturedRaid { get; set; }
        public WeeklyFeatured WeeklyPlaylist { get; set; }
        public List<RaidChallengeDay> RaidChallenges { get; set; }
        public DetailedChallengeOfElders EldersChallenge { get; set; }
        public DetailedWeeklyCrucible WeeklyCrucible { get; set; }
        public List<BountyDay> IronBannerBounties { get; set; }
        public List<RewardsDay> IronBannerRewards { get; set; }
        public DateTime ExpiryTime { get; set; }
        public DateTime StartTime { get; set; }
    }
}