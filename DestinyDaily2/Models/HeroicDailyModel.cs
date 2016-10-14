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
        public DateTime DisplayDate { get; set; }
        public string VisibleDate => DisplayDate.ToString("yyyy-MM-dd");
        public HeroicDailyDay DailyMission { get; set; }
        public List<Modifier> DailyModifiers { get; set; }
        public List<ManifestRewardModel> DailyRewards { get; set; }
        public CrucibleDailyDay DailyCrucible { get; set; }
        public List<ManifestRewardModel> DailyCrucibleRewards { get; set; }
    }
}