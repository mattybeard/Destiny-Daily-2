using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyApiManager.Models.D2.API
{
    public class AvailableQuest
    {
        public long questItemHash { get; set; }
        public Activity activity { get; set; }
        public List<Challenge> challenges { get; set; }
    }
}
