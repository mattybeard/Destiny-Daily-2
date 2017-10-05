using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DestinyDailyApiManager.Models.D2.Manifest;
using DestinyDailyDAL.Destiny2;

namespace DestinyDaily2.Models
{
    public class VendorsModel
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

        public DateTime StartTime { get; set; }
        public bool XurInTower { get; set; }
        public List<InventoryItemDefinition> XurSales { get; set; }
        public XurLocationDay XurLocation { get; set; }
    }
}