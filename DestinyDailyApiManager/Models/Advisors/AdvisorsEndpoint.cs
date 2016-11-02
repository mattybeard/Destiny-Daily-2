using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public int categoryHash { get; set; }
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
    }

    public class RewardItem
    {
        public object itemHash { get; set; }
        public int value { get; set; }
    }

    public class Reward
    {
        public List<RewardItem> rewardItems { get; set; }
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

    public class Skull
    {
        public string displayName { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
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
    }

    public class Extended
    {
        public List<Round> rounds { get; set; }
    }

    public class ActivityTier
    {
        public long activityHash { get; set; }
        public string tierDisplayName { get; set; }
        public List<Reward> rewards { get; set; }
        public ActivityData activityData { get; set; }
        public Extended extended { get; set; }
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
        public string expirationDate { get; set; }
        public string startDate { get; set; }
        public bool expirationKnown { get; set; }
        public bool active { get; set; }
    }

    public class Display2
    {
        public int categoryHash { get; set; }
        public string icon { get; set; }
        public string image { get; set; }
        public string advisorTypeCategory { get; set; }
        public long activityHash { get; set; }
        public int playlistHash { get; set; }
        public long destinationHash { get; set; }
        public long factionHash { get; set; }
        public long placeHash { get; set; }
        public string about { get; set; }
        public string status { get; set; }
        public List<string> tips { get; set; }
        public List<string> recruitmentIds { get; set; }
    }

    public class ActivityData2
    {
        public int activityHash { get; set; }
        public bool isNew { get; set; }
        public bool canLead { get; set; }
        public bool canJoin { get; set; }
        public bool isCompleted { get; set; }
        public bool isVisible { get; set; }
        public int displayLevel { get; set; }
        public int recommendedLight { get; set; }
        public int difficultyTier { get; set; }
    }

    public class Round2
    {
        public long enemyRaceHash { get; set; }
        public List<object> skullCategories { get; set; }
        public int bossCombatantHash { get; set; }
        public int bossLightLevel { get; set; }
    }

    public class Extended2
    {
        public List<Round2> rounds { get; set; }
    }

    public class ActivityTier2
    {
        public int activityHash { get; set; }
        public ActivityData2 activityData { get; set; }
        public Extended2 extended { get; set; }
    }

    public class Skull2
    {
        public string displayName { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class SkullCategory2
    {
        public string title { get; set; }
        public List<Skull2> skulls { get; set; }
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

    public class Extended3
    {
        public int highestWinRank { get; set; }
        public List<SkullCategory2> skullCategories { get; set; }
        public List<Objective> objectives { get; set; }
    }

    public class Elderchallenge
    {
        public string identifier { get; set; }
        public Status status { get; set; }
        public Display2 display { get; set; }
        public int vendorHash { get; set; }
        public long progressionHash { get; set; }
        public List<object> bountyHashes { get; set; }
        public List<ActivityTier2> activityTiers { get; set; }
        public Extended3 extended { get; set; }
    }

    public class Status2
    {
        public string expirationDate { get; set; }
        public string startDate { get; set; }
        public bool expirationKnown { get; set; }
        public bool active { get; set; }
    }

    public class Display3
    {
        public int categoryHash { get; set; }
        public string icon { get; set; }
        public string image { get; set; }
        public string flavor { get; set; }
        public string advisorTypeCategory { get; set; }
        public long activityHash { get; set; }
        public long playlistHash { get; set; }
        public long destinationHash { get; set; }
        public long placeHash { get; set; }
        public string about { get; set; }
        public string status { get; set; }
        public List<string> tips { get; set; }
        public List<string> recruitmentIds { get; set; }
    }

    public class ScoreCard
    {
        public bool hasTicket { get; set; }
        public int maxWins { get; set; }
        public int maxLosses { get; set; }
        public int wins { get; set; }
        public int losses { get; set; }
    }

    public class Extended4
    {
        public ScoreCard scoreCard { get; set; }
        public List<object> winRewardDetails { get; set; }
        public int highestWinRank { get; set; }
    }

    public class Trials
    {
        public string identifier { get; set; }
        public Status2 status { get; set; }
        public Display3 display { get; set; }
        public int vendorHash { get; set; }
        public List<object> bountyHashes { get; set; }
        public Extended4 extended { get; set; }
    }

    public class Status3
    {
        public string expirationDate { get; set; }
        public string startDate { get; set; }
        public bool expirationKnown { get; set; }
        public bool active { get; set; }
    }

    public class Display4
    {
        public int categoryHash { get; set; }
        public string icon { get; set; }
        public string image { get; set; }
        public string flavor { get; set; }
        public string advisorTypeCategory { get; set; }
        public int eventHash { get; set; }
        public int factionHash { get; set; }
        public string about { get; set; }
        public string status { get; set; }
        public List<string> tips { get; set; }
        public List<object> recruitmentIds { get; set; }
    }

    public class Extended5
    {
        public int highestWinRank { get; set; }
        public List<object> orders { get; set; }
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

    public class Status4
    {
        public string expirationDate { get; set; }
        public string startDate { get; set; }
        public bool expirationKnown { get; set; }
        public bool active { get; set; }
    }

    public class Display5
    {
        public long categoryHash { get; set; }
        public string icon { get; set; }
        public string image { get; set; }
        public string flavor { get; set; }
        public string advisorTypeCategory { get; set; }
        public long activityHash { get; set; }
        public long destinationHash { get; set; }
        public int factionHash { get; set; }
        public long placeHash { get; set; }
        public string about { get; set; }
        public string status { get; set; }
        public List<string> tips { get; set; }
        public List<string> recruitmentIds { get; set; }
    }

    public class RewardItem2
    {
        public long itemHash { get; set; }
        public int value { get; set; }
    }

    public class Reward2
    {
        public List<RewardItem2> rewardItems { get; set; }
    }

    public class ActivityData3
    {
        public long activityHash { get; set; }
        public bool isNew { get; set; }
        public bool canLead { get; set; }
        public bool canJoin { get; set; }
        public bool isCompleted { get; set; }
        public bool isVisible { get; set; }
        public int difficultyTier { get; set; }
    }

    public class ActivityTier3
    {
        public long activityHash { get; set; }
        public List<Reward2> rewards { get; set; }
        public ActivityData3 activityData { get; set; }
    }

    public class Weeklycrucible
    {
        public string identifier { get; set; }
        public Status4 status { get; set; }
        public Display5 display { get; set; }
        public long vendorHash { get; set; }
        public long progressionHash { get; set; }
        public List<long> bountyHashes { get; set; }
        public List<ActivityTier3> activityTiers { get; set; }
    }

    public class Status5
    {
        public string expirationDate { get; set; }
        public string startDate { get; set; }
        public bool expirationKnown { get; set; }
        public bool active { get; set; }
    }

    public class Display6
    {
        public int categoryHash { get; set; }
        public string icon { get; set; }
        public string image { get; set; }
        public string advisorTypeCategory { get; set; }
        public int activityHash { get; set; }
        public long destinationHash { get; set; }
        public int placeHash { get; set; }
        public string about { get; set; }
        public string status { get; set; }
        public List<string> tips { get; set; }
        public List<string> recruitmentIds { get; set; }
    }

    public class Step
    {
        public bool complete { get; set; }
    }

    public class RewardItem3
    {
        public long itemHash { get; set; }
        public int value { get; set; }
    }

    public class Reward3
    {
        public List<RewardItem3> rewardItems { get; set; }
    }

    public class ActivityData4
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

    public class ActivityTier4
    {
        public long activityHash { get; set; }
        public string tierDisplayName { get; set; }
        public List<Step> steps { get; set; }
        public List<object> skullCategories { get; set; }
        public List<Reward3> rewards { get; set; }
        public ActivityData4 activityData { get; set; }
    }

    public class Kingsfall
    {
        public string identifier { get; set; }
        public Status5 status { get; set; }
        public Display6 display { get; set; }
        public List<ActivityTier4> activityTiers { get; set; }
    }

    public class Status6
    {
        public string expirationDate { get; set; }
        public string startDate { get; set; }
        public bool expirationKnown { get; set; }
        public bool active { get; set; }
    }

    public class Display7
    {
        public int categoryHash { get; set; }
        public string icon { get; set; }
        public string image { get; set; }
        public string advisorTypeCategory { get; set; }
        public long activityHash { get; set; }
        public int destinationHash { get; set; }
        public long placeHash { get; set; }
        public string about { get; set; }
        public string status { get; set; }
        public List<string> tips { get; set; }
        public List<string> recruitmentIds { get; set; }
    }

    public class Step2
    {
        public bool complete { get; set; }
    }

    public class RewardItem4
    {
        public long itemHash { get; set; }
        public int value { get; set; }
    }

    public class Reward4
    {
        public List<RewardItem4> rewardItems { get; set; }
    }

    public class ActivityData5
    {
        public object activityHash { get; set; }
        public bool isNew { get; set; }
        public bool canLead { get; set; }
        public bool canJoin { get; set; }
        public bool isCompleted { get; set; }
        public bool isVisible { get; set; }
        public int displayLevel { get; set; }
        public int recommendedLight { get; set; }
        public int difficultyTier { get; set; }
    }

    public class ActivityTier5
    {
        public object activityHash { get; set; }
        public string tierDisplayName { get; set; }
        public List<Step2> steps { get; set; }
        public List<object> skullCategories { get; set; }
        public List<Reward4> rewards { get; set; }
        public ActivityData5 activityData { get; set; }
    }

    public class Vaultofglass
    {
        public string identifier { get; set; }
        public Status6 status { get; set; }
        public Display7 display { get; set; }
        public List<ActivityTier5> activityTiers { get; set; }
    }

    public class Status7
    {
        public string expirationDate { get; set; }
        public string startDate { get; set; }
        public bool expirationKnown { get; set; }
        public bool active { get; set; }
    }

    public class Display8
    {
        public int categoryHash { get; set; }
        public string icon { get; set; }
        public string image { get; set; }
        public string advisorTypeCategory { get; set; }
        public int activityHash { get; set; }
        public long destinationHash { get; set; }
        public long placeHash { get; set; }
        public string about { get; set; }
        public string status { get; set; }
        public List<string> tips { get; set; }
        public List<string> recruitmentIds { get; set; }
    }

    public class Step3
    {
        public bool complete { get; set; }
    }

    public class RewardItem5
    {
        public object itemHash { get; set; }
        public int value { get; set; }
    }

    public class Reward5
    {
        public List<RewardItem5> rewardItems { get; set; }
    }

    public class ActivityData6
    {
        public int activityHash { get; set; }
        public bool isNew { get; set; }
        public bool canLead { get; set; }
        public bool canJoin { get; set; }
        public bool isCompleted { get; set; }
        public bool isVisible { get; set; }
        public int displayLevel { get; set; }
        public int recommendedLight { get; set; }
        public int difficultyTier { get; set; }
    }

    public class ActivityTier6
    {
        public int activityHash { get; set; }
        public string tierDisplayName { get; set; }
        public List<Step3> steps { get; set; }
        public List<object> skullCategories { get; set; }
        public List<Reward5> rewards { get; set; }
        public ActivityData6 activityData { get; set; }
    }

    public class Crota
    {
        public string identifier { get; set; }
        public Status7 status { get; set; }
        public Display8 display { get; set; }
        public List<ActivityTier6> activityTiers { get; set; }
    }

    public class Status8
    {
        public string expirationDate { get; set; }
        public string startDate { get; set; }
        public bool expirationKnown { get; set; }
        public bool active { get; set; }
    }

    public class Display9
    {
        public int categoryHash { get; set; }
        public string icon { get; set; }
        public string image { get; set; }
        public string advisorTypeCategory { get; set; }
        public long activityHash { get; set; }
        public long playlistHash { get; set; }
        public long destinationHash { get; set; }
        public int factionHash { get; set; }
        public long placeHash { get; set; }
        public string about { get; set; }
        public string status { get; set; }
        public List<string> tips { get; set; }
        public List<string> recruitmentIds { get; set; }
    }

    public class RewardItem6
    {
        public long itemHash { get; set; }
        public int value { get; set; }
    }

    public class Reward6
    {
        public List<RewardItem6> rewardItems { get; set; }
    }

    public class ActivityData7
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

    public class ActivityTier7
    {
        public long activityHash { get; set; }
        public string tierDisplayName { get; set; }
        public List<Reward6> rewards { get; set; }
        public ActivityData7 activityData { get; set; }
    }

    public class Skull3
    {
        public string displayName { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class SkullCategory3
    {
        public string title { get; set; }
        public List<Skull3> skulls { get; set; }
    }

    public class Extended6
    {
        public int highestWinRank { get; set; }
        public List<SkullCategory3> skullCategories { get; set; }
    }

    public class Nightfall
    {
        public string identifier { get; set; }
        public Status8 status { get; set; }
        public Display9 display { get; set; }
        public long vendorHash { get; set; }
        public long progressionHash { get; set; }
        public List<object> bountyHashes { get; set; }
        public List<ActivityTier7> activityTiers { get; set; }
        public Extended6 extended { get; set; }
    }

    public class Status9
    {
        public string expirationDate { get; set; }
        public string startDate { get; set; }
        public bool expirationKnown { get; set; }
        public bool active { get; set; }
    }

    public class Display10
    {
        public int categoryHash { get; set; }
        public string icon { get; set; }
        public string image { get; set; }
        public string advisorTypeCategory { get; set; }
        public long activityHash { get; set; }
        public long destinationHash { get; set; }
        public int factionHash { get; set; }
        public long placeHash { get; set; }
        public string about { get; set; }
        public string status { get; set; }
        public List<string> tips { get; set; }
        public List<string> recruitmentIds { get; set; }
    }

    public class RewardItem7
    {
        public long itemHash { get; set; }
        public int value { get; set; }
    }

    public class Reward7
    {
        public List<RewardItem7> rewardItems { get; set; }
    }

    public class ActivityData8
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

    public class ActivityTier8
    {
        public long activityHash { get; set; }
        public string tierDisplayName { get; set; }
        public List<Reward7> rewards { get; set; }
        public ActivityData8 activityData { get; set; }
    }

    public class Skull4
    {
        public string displayName { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class SkullCategory4
    {
        public string title { get; set; }
        public List<Skull4> skulls { get; set; }
    }

    public class Extended7
    {
        public int highestWinRank { get; set; }
        public List<SkullCategory4> skullCategories { get; set; }
    }

    public class Heroicstrike
    {
        public string identifier { get; set; }
        public Status9 status { get; set; }
        public Display10 display { get; set; }
        public long vendorHash { get; set; }
        public long progressionHash { get; set; }
        public List<object> bountyHashes { get; set; }
        public List<ActivityTier8> activityTiers { get; set; }
        public Extended7 extended { get; set; }
    }

    public class Status10
    {
        public string expirationDate { get; set; }
        public string startDate { get; set; }
        public bool expirationKnown { get; set; }
        public bool active { get; set; }
    }

    public class Display11
    {
        public long categoryHash { get; set; }
        public string icon { get; set; }
        public string image { get; set; }
        public string advisorTypeCategory { get; set; }
        public long activityHash { get; set; }
        public long destinationHash { get; set; }
        public int factionHash { get; set; }
        public long placeHash { get; set; }
        public string about { get; set; }
        public string status { get; set; }
        public List<string> tips { get; set; }
        public List<string> recruitmentIds { get; set; }
    }

    public class RewardItem8
    {
        public long itemHash { get; set; }
        public int value { get; set; }
    }

    public class Reward8
    {
        public List<RewardItem8> rewardItems { get; set; }
    }

    public class ActivityData9
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

    public class ActivityTier9
    {
        public long activityHash { get; set; }
        public string tierDisplayName { get; set; }
        public List<Reward8> rewards { get; set; }
        public ActivityData9 activityData { get; set; }
    }

    public class Dailychapter
    {
        public string identifier { get; set; }
        public Status10 status { get; set; }
        public Display11 display { get; set; }
        public long vendorHash { get; set; }
        public long progressionHash { get; set; }
        public List<long> bountyHashes { get; set; }
        public List<ActivityTier9> activityTiers { get; set; }
    }

    public class Status11
    {
        public string expirationDate { get; set; }
        public string startDate { get; set; }
        public bool expirationKnown { get; set; }
        public bool active { get; set; }
    }

    public class Display12
    {
        public long categoryHash { get; set; }
        public string icon { get; set; }
        public string image { get; set; }
        public string advisorTypeCategory { get; set; }
        public long activityHash { get; set; }
        public long destinationHash { get; set; }
        public int factionHash { get; set; }
        public long placeHash { get; set; }
        public string about { get; set; }
        public string status { get; set; }
        public List<string> tips { get; set; }
        public List<string> recruitmentIds { get; set; }
    }

    public class ActivityData10
    {
        public long activityHash { get; set; }
        public bool isNew { get; set; }
        public bool canLead { get; set; }
        public bool canJoin { get; set; }
        public bool isCompleted { get; set; }
        public bool isVisible { get; set; }
        public int difficultyTier { get; set; }
    }

    public class ActivityTier10
    {
        public long activityHash { get; set; }
        public ActivityData10 activityData { get; set; }
    }

    public class Dailycrucible
    {
        public string identifier { get; set; }
        public Status11 status { get; set; }
        public Display12 display { get; set; }
        public long vendorHash { get; set; }
        public int progressionHash { get; set; }
        public List<long> bountyHashes { get; set; }
        public List<ActivityTier10> activityTiers { get; set; }
    }

    public class Status12
    {
        public bool expirationKnown { get; set; }
        public bool active { get; set; }
    }

    public class Display13
    {
        public int categoryHash { get; set; }
        public string icon { get; set; }
        public string image { get; set; }
        public string flavor { get; set; }
        public string advisorTypeCategory { get; set; }
        public int activityHash { get; set; }
        public int playlistHash { get; set; }
        public long destinationHash { get; set; }
        public long factionHash { get; set; }
        public long placeHash { get; set; }
        public string about { get; set; }
        public string status { get; set; }
        public List<string> tips { get; set; }
        public List<object> recruitmentIds { get; set; }
    }

    public class RewardItem9
    {
        public object itemHash { get; set; }
        public int value { get; set; }
    }

    public class Reward9
    {
        public List<RewardItem9> rewardItems { get; set; }
    }

    public class ActivityData11
    {
        public int activityHash { get; set; }
        public bool isNew { get; set; }
        public bool canLead { get; set; }
        public bool canJoin { get; set; }
        public bool isCompleted { get; set; }
        public bool isVisible { get; set; }
        public int displayLevel { get; set; }
        public int recommendedLight { get; set; }
        public int difficultyTier { get; set; }
    }

    public class ActivityTier11
    {
        public int activityHash { get; set; }
        public string tierDisplayName { get; set; }
        public List<object> skullCategories { get; set; }
        public List<Reward9> rewards { get; set; }
        public ActivityData11 activityData { get; set; }
    }

    public class PrisonOfEldersPlaylist
    {
        public string identifier { get; set; }
        public Status12 status { get; set; }
        public Display13 display { get; set; }
        public int vendorHash { get; set; }
        public List<ActivityTier11> activityTiers { get; set; }
    }

    public class Status13
    {
        public bool expirationKnown { get; set; }
        public bool active { get; set; }
    }

    public class Display14
    {
        public int categoryHash { get; set; }
        public string icon { get; set; }
        public string image { get; set; }
        public string flavor { get; set; }
        public string advisorTypeCategory { get; set; }
        public int activityHash { get; set; }
        public int eventHash { get; set; }
        public int playlistHash { get; set; }
        public long factionHash { get; set; }
        public string about { get; set; }
        public string status { get; set; }
        public List<string> tips { get; set; }
        public List<string> recruitmentIds { get; set; }
    }

    public class Ironbanner
    {
        public string identifier { get; set; }
        public Status13 status { get; set; }
        public Display14 display { get; set; }
        public long vendorHash { get; set; }
        public long progressionHash { get; set; }
        public List<object> bountyHashes { get; set; }
        public List<object> questHashes { get; set; }
    }

    public class Status14
    {
        public string expirationDate { get; set; }
        public string startDate { get; set; }
        public bool expirationKnown { get; set; }
        public bool active { get; set; }
    }

    public class Display15
    {
        public int categoryHash { get; set; }
        public string icon { get; set; }
        public string image { get; set; }
        public string flavor { get; set; }
        public string advisorTypeCategory { get; set; }
        public int eventHash { get; set; }
        public List<object> recruitmentIds { get; set; }
    }

    public class Xur
    {
        public string identifier { get; set; }
        public Status14 status { get; set; }
        public Display15 display { get; set; }
        public long vendorHash { get; set; }
        public List<object> bountyHashes { get; set; }
        public List<object> questHashes { get; set; }
    }

    public class Status15
    {
        public bool expirationKnown { get; set; }
        public bool active { get; set; }
    }

    public class Display16
    {
        public int categoryHash { get; set; }
        public string icon { get; set; }
        public string image { get; set; }
        public string flavor { get; set; }
        public string advisorTypeCategory { get; set; }
        public int eventHash { get; set; }
        public long factionHash { get; set; }
        public List<string> recruitmentIds { get; set; }
    }

    public class Srl
    {
        public string identifier { get; set; }
        public Status15 status { get; set; }
        public Display16 display { get; set; }
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
