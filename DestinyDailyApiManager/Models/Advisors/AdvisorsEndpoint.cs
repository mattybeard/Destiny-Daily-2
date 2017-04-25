using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinyDailyApiManager.Models.Manifest.Activity;

// ReSharper disable InconsistentNaming

namespace DestinyDailyApiManager.Models.Advisors
{
    public class AdvisorsEndpoint
    {
        public Response Response { get; set; }
        public int ErrorCode { get; set; }
        public int ThrottleSeconds { get; set; }
        public string ErrorStatus { get; set; }
        public string Message { get; set; }
        public MessageData MessageData { get; set; }
    }

    public class Display
    {
        public long categoryHash { get; set; }
        public string icon { get; set; }
        public string image { get; set; }
        public string advisorTypeCategory { get; set; }
        public long activityHash { get; set; }
        public long destinationHash { get; set; }
        public long factionHash { get; set; }
        public long placeHash { get; set; }
        public string about { get; set; }
        public string status { get; set; }
        public List<string> tips { get; set; }
        public List<string> recruitmentIds { get; set; }
        public string flavor { get; set; }
        public long playlistHash { get; set; }
    }
    public class ActivityData
    {
        public long activityHash { get; set; }
        public bool isNew { get; set; }
        public bool canLead { get; set; }
        public bool canJoin { get; set; }
        public bool isCompleted { get; set; }
        public bool isVisible { get; set; }
        public int displayLevel { get; set; }
        public int recommendedLight { get; set; }
        public int difficultyTier { get; set; }
    }

    public class SkullCategory
    {
        public string title { get; set; }
        public List<Skull> skulls { get; set; }
    }

    public class Round
    {
        public long enemyRaceHash { get; set; }
        public List<SkullCategory> skullCategories { get; set; }
        public int bossCombatantHash { get; set; }
        public int bossLightLevel { get; set; }
    }

    public class Extended
    {
        public List<Round> rounds { get; set; }
        public ScoreCard scoreCard { get; set; }
        public List<object> winRewardDetails { get; set; }
        public int highestWinRank { get; set; }
        public List<SkullCategory> skullCategories { get; set; }
        public List<Objective> objectives { get; set; }

        public List<object> orders { get; set; }
    }

    public class ActivityTier
    {
        public long activityHash { get; set; }
        public string tierDisplayName { get; set; }
        public List<Reward> rewards { get; set; }
        public ActivityData activityData { get; set; }
        public Extended extended { get; set; }
        public List<object> skullCategories { get; set; }

        public List<Step> steps { get; set; }
    }

    public class Prisonofelders
    {
        public string identifier { get; set; }
        public Display display { get; set; }
        public int vendorHash { get; set; }
        public long progressionHash { get; set; }
        public List<ActivityTier> activityTiers { get; set; }
    }

    public class Status
    {
        public DateTime expirationDate { get; set; }
        public DateTime startDate { get; set; }
        public bool expirationKnown { get; set; }
        public bool active { get; set; }
    }

    public class Objective
    {
        public object objectiveHash { get; set; }
        public int destinationHash { get; set; }
        public int activityHash { get; set; }
        public int progress { get; set; }
        public bool hasProgress { get; set; }
        public bool isComplete { get; set; }
    }

    public class Elderchallenge
    {
        public string identifier { get; set; }
        public Status status { get; set; }
        public Display display { get; set; }
        public int vendorHash { get; set; }
        public long progressionHash { get; set; }
        public List<long> bountyHashes { get; set; }
        public List<ActivityTier> activityTiers { get; set; }
        public Extended extended { get; set; }
    }

    public class ScoreCard
    {
        public bool hasTicket { get; set; }
        public int maxWins { get; set; }
        public int maxLosses { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
    }
    
    public class Trials
    {
        public string identifier { get; set; }
        public Status status { get; set; }
        public Display display { get; set; }
        public int vendorHash { get; set; }
        public List<long> bountyHashes { get; set; }
        public Extended extended { get; set; }
    }

    public class Armsday
    {
        public string identifier { get; set; }
        public Status status { get; set; }
        public Display display { get; set; }
        public int vendorHash { get; set; }
        public long progressionHash { get; set; }
        public Extended extended { get; set; }
    }

    public class Wrathofthemachine
    {
        public string identifier { get; set; }
        public Status status { get; set; }
        public Display display { get; set; }
        public List<ActivityTier> activityTiers { get; set; }
    }

    public class Weeklycrucible
    {
        public string identifier { get; set; }
        public Status status { get; set; }
        public Display display { get; set; }
        public long vendorHash { get; set; }
        public long progressionHash { get; set; }
        public List<long> bountyHashes { get; set; }
        public List<ActivityTier> activityTiers { get; set; }
    }

    public class Step
    {
        public bool complete { get; set; }
    }

    public class Kingsfall
    {
        public string identifier { get; set; }
        public Status status { get; set; }
        public Display display { get; set; }
        public List<ActivityTier> activityTiers { get; set; }
    }

    public class Vaultofglass
    {
        public string identifier { get; set; }
        public Status status { get; set; }
        public Display display { get; set; }
        public List<ActivityTier> activityTiers { get; set; }
    }   

    public class Crota
    {
        public string identifier { get; set; }
        public Status status { get; set; }
        public Display display { get; set; }
        public List<ActivityTier> activityTiers { get; set; }
    }

    public class Nightfall
    {
        public string identifier { get; set; }
        public Status status { get; set; }
        public Display display { get; set; }
        public long vendorHash { get; set; }
        public long progressionHash { get; set; }
        public List<long> bountyHashes { get; set; }
        public List<ActivityTier> activityTiers { get; set; }
        public Extended extended { get; set; }
    }

    public class Heroicstrike
    {
        public string identifier { get; set; }
        public Status status { get; set; }
        public Display display { get; set; }
        public long vendorHash { get; set; }
        public long progressionHash { get; set; }
        public List<object> bountyHashes { get; set; }
        public List<ActivityTier> activityTiers { get; set; }
        public Extended extended { get; set; }
    }    

    public class Dailychapter
    {
        public string identifier { get; set; }
        public Status status { get; set; }
        public Display display { get; set; }
        public long vendorHash { get; set; }
        public long progressionHash { get; set; }
        public List<long> bountyHashes { get; set; }
        public List<ActivityTier> activityTiers { get; set; }
    }
  
    public class Dailycrucible
    {
        public string identifier { get; set; }
        public Status status { get; set; }
        public Display display { get; set; }
        public long vendorHash { get; set; }
        public int progressionHash { get; set; }
        public List<long> bountyHashes { get; set; }
        public List<ActivityTier> activityTiers { get; set; }
    }


    public class PrisonOfEldersPlaylist
    {
        public string identifier { get; set; }
        public Status status { get; set; }
        public Display display { get; set; }
        public int vendorHash { get; set; }
        public List<ActivityTier> activityTiers { get; set; }
    }
 
    public class Ironbanner
    {
        public string identifier { get; set; }
        public Status status { get; set; }
        public Display display { get; set; }
        public long vendorHash { get; set; }
        public long progressionHash { get; set; }
        public List<object> bountyHashes { get; set; }
        public List<object> questHashes { get; set; }
    }

    public class Xur
    {
        public string identifier { get; set; }
        public Status status { get; set; }
        public Display display { get; set; }
        public long vendorHash { get; set; }
        public List<long> bountyHashes { get; set; }
        public List<long> questHashes { get; set; }
    }

    public class Srl
    {
        public string identifier { get; set; }
        public Status status { get; set; }
        public Display display { get; set; }
        public int vendorHash { get; set; }
        public long progressionHash { get; set; }
        public List<object> bountyHashes { get; set; }
        public List<object> questHashes { get; set; }
    }

    public class Activities
    {
        public Prisonofelders prisonofelders { get; set; }
        public Elderchallenge elderchallenge { get; set; }
        public Trials trials { get; set; }
        public Armsday armsday { get; set; }
        public Weeklycrucible weeklycrucible { get; set; }
        public Wrathofthemachine wrathofthemachine { get; set; }   
        public Kingsfall kingsfall { get; set; }
        public Vaultofglass vaultofglass { get; set; }
        public Crota crota { get; set; }
        public Nightfall nightfall { get; set; }
        public Heroicstrike heroicstrike { get; set; }
        public Dailychapter dailychapter { get; set; }
        public Dailycrucible dailycrucible { get; set; }
        public PrisonOfEldersPlaylist prisonofeldersplaylist { get; set; }
        public Ironbanner ironbanner { get; set; }
        public Xur xur { get; set; }
        public Srl srl { get; set; }
        public Weeklystory weeklystory { get; set; }
    }

    public class Weeklystory
    {
        public string identifier { get; set; }
        public Status status { get; set; }
        public Display display { get; set; }
        public long vendorHash { get; set; }
        public long progressionHash { get; set; }
        public List<long> bountyHashes { get; set; }
        public List<ActivityTier> activityTiers { get; set; }
        public Extended extended { get; set; }
    }


    public class Data
    {
        public Activities activities { get; set; }
        //public ActivityCategories activityCategories { get; set; }
    }

    public class Response
    {
        public Data data { get; set; }
    }

    public class MessageData
    {
    }
}
