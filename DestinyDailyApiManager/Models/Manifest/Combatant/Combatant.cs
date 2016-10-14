using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyApiManager.Models.Manifest.Combatant
{
    public class Combatant
    {
        public long combatantHash { get; set; }
        public string combatantName { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public string image { get; set; }
        public long hash { get; set; }
        public int index { get; set; }
        public bool redacted { get; set; }
    }
}
