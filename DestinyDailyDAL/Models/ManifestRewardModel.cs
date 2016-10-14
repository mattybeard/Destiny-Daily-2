using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace DestinyDailyDAL.Models
{
    public class ManifestRewardModel
    {
        public InventoryItem Item { get; set; }
        public int Quantity { get; set; }
    }
}