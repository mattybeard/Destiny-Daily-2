using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyApiManager.Models.D2.API
{
    public class ActivityDefinition
    {
        public DisplayProperties displayProperties { get; set; }
        public string releaseIcon { get; set; }
        public int releaseTime { get; set; }
        public int activityLevel { get; set; }
        public long completionUnlockHash { get; set; }
        public int activityLightLevel { get; set; }
        public long destinationHash { get; set; }
        public long placeHash { get; set; }
        public long activityTypeHash { get; set; }
        public int tier { get; set; }
        public string pgcrImage { get; set; }
        public List<Reward> rewards { get; set; }
        public List<ActivityModifier> modifiers { get; set; }
        public bool isPlaylist { get; set; }
        public List<Challenge> challenges { get; set; }
        public List<object> optionalUnlockStrings { get; set; }
        public bool inheritFromFreeRoam { get; set; }
        public bool suppressOtherRewards { get; set; }
        public List<object> playlistItems { get; set; }
        public Matchmaking matchmaking { get; set; }
        public long activityModeHash { get; set; }
        public bool isPvP { get; set; }
        public long hash { get; set; }
        public int index { get; set; }
        public bool redacted { get; set; }
    }
}
