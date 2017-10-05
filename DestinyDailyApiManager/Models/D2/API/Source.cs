using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyApiManager.Models.D2.API
{
    public class Source
    {
        public int level { get; set; }
        public int minQuality { get; set; }
        public int maxQuality { get; set; }
        public int minLevelRequired { get; set; }
        public int maxLevelRequired { get; set; }
        public int exclusivity { get; set; }
        public List<long> sourceHashes { get; set; }
    }
}
