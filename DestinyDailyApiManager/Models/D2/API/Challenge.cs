using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyApiManager.Models.D2.API
{
    public class Challenge
    {
        public long objectiveHash { get; set; }
        public long activityHash { get; set; }
        public int rewardSiteHash { get; set; }
        public int inhibitRewardsUnlockHash { get; set; }
    }
}
