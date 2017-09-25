using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyApiManager.Models.D2.Manifest
{
    public class Inventory
    {
        public string stackUniqueLabel { get; set; }
        public int maxStackSize { get; set; }
        public long bucketTypeHash { get; set; }
        public long recoveryBucketTypeHash { get; set; }
        public long tierTypeHash { get; set; }
        public bool isInstanceItem { get; set; }
        public bool nonTransferrableOriginal { get; set; }
        public string tierTypeName { get; set; }
        public int tierType { get; set; }
    }
}
