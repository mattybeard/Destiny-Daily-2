using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DestinyDailyDAL.Destiny2.Models;

namespace DestinyDaily2.Models
{
    public class NightfallModel
    {
        public DateTime ExpiryTime { get; set; }
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

        public NightfallManifestModel Nightfall { get; set; }
    }
}