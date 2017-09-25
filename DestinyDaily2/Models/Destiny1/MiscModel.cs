using System.Collections.Generic;
using DestinyDailyDAL.Destiny1;

namespace DestinyDaily2.Models.Destiny1
{
    public class MiscModel
    {
        public List<MaterialExchange> MaterialExchanges { get; set; } 
        public List<RaidChallengeDay> RaidChallenges { get; set; } 
    }
}