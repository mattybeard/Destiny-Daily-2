using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyApiManager.Models.D2.API
{
    public class SourceData
    {
        public List<long> sourceHashes { get; set; }
        public List<Source> sources { get; set; }
        public int exclusive { get; set; }
    }
}
