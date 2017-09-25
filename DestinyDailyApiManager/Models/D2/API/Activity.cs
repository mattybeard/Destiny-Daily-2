using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyApiManager.Models.D2.API
{
    public class Activity
    {
        public long activityHash { get; set; }
        public List<long> modifierHashes { get; set; }
        public List<Variant> variants { get; set; }
    }
}
