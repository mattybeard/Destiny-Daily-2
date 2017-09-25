using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyApiManager.Models.D2.API
{
    public class Matchmaking
    {
        public bool isMatchmade { get; set; }
        public int minParty { get; set; }
        public int maxParty { get; set; }
        public int maxPlayers { get; set; }
        public bool requiresGuardianOath { get; set; }
    }
}
