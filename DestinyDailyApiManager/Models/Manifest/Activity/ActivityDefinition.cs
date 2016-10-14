using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyApiManager.Models.Manifest.Activity
{
    public class RewardItem
    {
        public long itemHash { get; set; }
        public int value { get; set; }
    }

    public class Reward
    {
        public List<RewardItem> rewardItems { get; set; }
    }

    public class Skull
    {
        public string displayName { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class ActivityDefinition
    {
        public long activityHash { get; set; }
        public string activityName { get; set; }
        public string activityDescription { get; set; }
        public string icon { get; set; }
        public string releaseIcon { get; set; }
        public int releaseTime { get; set; }
        public int activityLevel { get; set; }
        public long completionFlagHash { get; set; }
        public double activityPower { get; set; }
        public int minParty { get; set; }
        public int maxParty { get; set; }
        public int maxPlayers { get; set; }
        public long destinationHash { get; set; }
        public long placeHash { get; set; }
        public long activityTypeHash { get; set; }
        public int tier { get; set; }
        public string pgcrImage { get; set; }
        public List<Reward> rewards { get; set; }
        public List<Skull> skulls { get; set; }
        public bool isPlaylist { get; set; }
        public bool isMatchmade { get; set; }
        public long hash { get; set; }
        public int index { get; set; }
        public bool redacted { get; set; }
    }
}
