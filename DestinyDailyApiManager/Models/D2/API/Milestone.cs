using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyApiManager.Models.D2.API
{
    public class Milestone
    {
        public long milestoneHash { get; set; }
        public List<AvailableQuest> availableQuests { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
