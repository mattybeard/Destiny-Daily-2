using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyDAL.Models
{
    public class DetailedWeeklyCrucible
    {
        public CrucibleWeeklyDay Weekly { get; set; }
        public List<BountyDay> Bounties { get; set; }
    }
}
