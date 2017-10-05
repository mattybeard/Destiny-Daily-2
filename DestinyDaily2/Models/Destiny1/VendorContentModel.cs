using System;
using System.Collections.Generic;
using DestinyDailyDAL.Destiny1;

namespace DestinyDaily2.Models.Destiny1
{
    public class VendorContentModel
    {
        public bool HideSrl { get; set; }
        public DateTime ExpiryTime { get; set; }
        public List<XurD1Day> XurSales { get; set; }
        public bool XurInTower { get; set; }
        public XurD1LocationDay XurLocation { get; set; }
        public List<MaterialExchange> MaterialExchanges { get; set; }
        public List<InventoryItem> MaterialDetail { get; set; }
        public TrialsMapDay TrialsDetails { get; set; }
        public List<BountyDay> IronLordBounties { get; set; }
        public List<RewardsDay> IronLordArtifacts { get; set; }
        public List<BountyDay> SrlBounties { get; set; }
        public List<RewardsDay> SrlRewards { get; set; }
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