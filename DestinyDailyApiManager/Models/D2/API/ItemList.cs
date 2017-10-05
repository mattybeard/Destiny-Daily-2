using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming

namespace DestinyDailyApiManager.Models.D2.API
{
    public class ItemList
    {
        public int vendorItemIndex { get; set; }
        public long itemHash { get; set; }
        public int quantity { get; set; }
        public List<long> failureIndexes { get; set; }
        public bool priceOverrideEnabled { get; set; }
        public int refundPolicy { get; set; }
        public int refundTimeLimit { get; set; }
        public int rewardAdjustorPointerHash { get; set; }
        public int displayCategoryIndex { get; set; }
        public int seedOverride { get; set; }
        public int categoryIndex { get; set; }
        public int originalCategoryIndex { get; set; }
        public int weight { get; set; }
        public int minimumLevel { get; set; }
        public int maximumLevel { get; set; }
        public long licenseUnlockHash { get; set; }
        public string displayCategory { get; set; }
        public object inventoryBucketHash { get; set; }
        public int visibilityScope { get; set; }
        public int purchasableScope { get; set; }
    }
}
