using System;
using System.Collections.Generic;
using DestinyDailyDAL.Destiny1;

namespace DestinyDaily2.Models.Destiny1
{
    public class NightfallDataModel
    {
        public Nightfall ThisNightfall { get; set; }
        public List<BountyDay> WeeklyStrikeBounties { get; set; }
        public DateTime ExpiryTime { get; set; }
        public DateTime StartTime { get; set; }
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