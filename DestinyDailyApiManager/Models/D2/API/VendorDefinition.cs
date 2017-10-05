using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming

namespace DestinyDailyApiManager.Models.D2.API
{
    public class VendorDefinition
    {
        public DisplayProperties displayProperties { get; set; }
        public long displayItemHash { get; set; }
        public bool inhibitBuying { get; set; }
        public bool inhibitSelling { get; set; }
        public long factionHash { get; set; }
        public int resetIntervalMinutes { get; set; }
        public int resetOffsetMinutes { get; set; }
        public List<object> failureStrings { get; set; }
        public List<object> unlockRanges { get; set; }
        public bool enabled { get; set; }
        public bool visible { get; set; }
        public bool consolidateCategories { get; set; }
        public long unlockValueHash { get; set; }
        public List<object> actions { get; set; }
        public List<object> interactions { get; set; }
        public List<object> inventoryFlyouts { get; set; }
        public List<ItemList> itemList { get; set; }
        public List<object> services { get; set; }
        public List<object> acceptedItems { get; set; }
        public long hash { get; set; }
        public int index { get; set; }
        public bool redacted { get; set; }
    }
}
