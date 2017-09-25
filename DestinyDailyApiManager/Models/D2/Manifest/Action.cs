using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyApiManager.Models.D2.Manifest
{
    public class Action
    {
        public string verbName { get; set; }
        public bool isPositive { get; set; }
        public int requiredCooldownSeconds { get; set; }
        public List<long> requiredItems { get; set; }
        //public List<long> progressionRewards { get; set; }
        public long rewardSheetHash { get; set; }
        public long rewardItemHash { get; set; }
        public long rewardSiteHash { get; set; }
        public long requiredCooldownHash { get; set; }
        public bool deleteOnAction { get; set; }
        public bool consumeEntireStack { get; set; }
        public bool useOnAcquire { get; set; }
    }
}
