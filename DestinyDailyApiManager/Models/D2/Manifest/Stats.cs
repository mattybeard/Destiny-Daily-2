using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyApiManager.Models.D2.Manifest
{
    public class Stats
    {
        public Stats2 stats { get; set; }
        public bool hasDisplayableStats { get; set; }
        public long primaryBaseStatHash { get; set; }
    }
}
