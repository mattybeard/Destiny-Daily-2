﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinyDailyApiManager.Models.D2.Manifest;
// ReSharper disable InconsistentNaming

namespace DestinyDailyApiManager.Models.D2.API
{
    class InventoryItemDefinition
    {
        public DisplayProperties displayProperties { get; set; }
        public string screenshot { get; set; }
        public string itemTypeDisplayName { get; set; }
        public string itemTypeAndTierDisplayName { get; set; }
        public string displaySource { get; set; }
        public Action action { get; set; }
        public Inventory inventory { get; set; }
        public EquippingBlock equippingBlock { get; set; }
        public Quality quality { get; set; }
        public SourceData sourceData { get; set; }
        public List<object> perks { get; set; }
        public bool allowActions { get; set; }
        public bool nonTransferrable { get; set; }
        public List<long> itemCategoryHashes { get; set; }
        public int specialItemType { get; set; }
        public int itemType { get; set; }
        public int itemSubType { get; set; }
        public int classType { get; set; }
        public bool equippable { get; set; }
        public int defaultDamageType { get; set; }
        public long hash { get; set; }
        public int index { get; set; }
        public bool redacted { get; set; }
    }
}
