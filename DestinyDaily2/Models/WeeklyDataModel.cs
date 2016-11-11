using System;
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
        public List<RaidChallengeDay> RaidChallenges { get; set; }
        public DetailedChallengeOfElders EldersChallenge { get; set; }
        public DetailedWeeklyCrucible WeeklyCrucible { get; set; }
    }
}