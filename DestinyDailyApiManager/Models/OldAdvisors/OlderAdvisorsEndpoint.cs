using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyApiManager.Models.OldAdvisors
{
    public class HeroicStrikeRewardIndexes
    {
        //public List<int> __invalid_name__3476387771 { get; set; }
    }

    public class DailyChapterRewardIndexes
    {
        //public List<int> __invalid_name__2507671226 { get; set; }
    }

    public class Round
    {
        public object enemyRaceHash { get; set; }
        public List<int> skulls { get; set; }
    }

    public class Arena
    {
        public long activityHash { get; set; }
        public string iconPath { get; set; }
        public List<Round> rounds { get; set; }
        public bool bossFight { get; set; }
        public List<object> bossSkulls { get; set; }
        public List<int> activeRewardIndexes { get; set; }
        public bool isCompleted { get; set; }
    }

    public class AckState
    {
        public bool needsAck { get; set; }
        public string ackId { get; set; }
    }

    public class ArtRegions
    {
    }

    public class PrimaryStat
    {
        public object statHash { get; set; }
        public int value { get; set; }
        public int maximumValue { get; set; }
    }

    public class Progression
    {
        public int dailyProgress { get; set; }
        public int weeklyProgress { get; set; }
        public int currentProgress { get; set; }
        public int level { get; set; }
        public int step { get; set; }
        public int progressToNextLevel { get; set; }
        public int nextLevelAt { get; set; }
        public int progressionHash { get; set; }
    }

    public class Item
    {
        public long itemHash { get; set; }
        public int bindStatus { get; set; }
        public bool isEquipped { get; set; }
        public string itemInstanceId { get; set; }
        public int itemLevel { get; set; }
        public int stackSize { get; set; }
        public int qualityLevel { get; set; }
        public List<object> stats { get; set; }
        public bool canEquip { get; set; }
        public int equipRequiredLevel { get; set; }
        public object unlockFlagHashRequiredToEquip { get; set; }
        public int cannotEquipReason { get; set; }
        public int damageType { get; set; }
        public long damageTypeHash { get; set; }
        public int damageTypeNodeIndex { get; set; }
        public int damageTypeStepIndex { get; set; }
        public object talentGridHash { get; set; }
        public List<object> nodes { get; set; }
        public bool useCustomDyes { get; set; }
        public ArtRegions artRegions { get; set; }
        public bool isEquipment { get; set; }
        public bool isGridComplete { get; set; }
        public List<object> perks { get; set; }
        public int location { get; set; }
        public int transferStatus { get; set; }
        public bool locked { get; set; }
        public bool lockable { get; set; }
        public List<object> objectives { get; set; }
        public int state { get; set; }
        public PrimaryStat primaryStat { get; set; }
        public Progression progression { get; set; }
    }

    public class Cost
    {
        public int itemHash { get; set; }
        public int value { get; set; }
    }

    public class SaleItem
    {
        public Item item { get; set; }
        public int vendorItemIndex { get; set; }
        public int itemStatus { get; set; }
        public List<Cost> costs { get; set; }
        public List<object> failureIndexes { get; set; }
    }

    public class SaleItemCategory
    {
        public int categoryIndex { get; set; }
        public string categoryTitle { get; set; }
        public List<SaleItem> saleItems { get; set; }
    }

    public class Vendor
    {
        public long vendorHash { get; set; }
        public AckState ackState { get; set; }
        public string nextRefreshDate { get; set; }
        public bool enabled { get; set; }
        public List<SaleItemCategory> saleItemCategories { get; set; }
        public List<object> inventoryBuckets { get; set; }
        public bool canPurchase { get; set; }
    }

    public class Bounties
    {
    }

    public class Quests
    {
    }

    public class Event
    {
        public int eventHash { get; set; }
        public string friendlyIdentifier { get; set; }
        public string eventIdentifier { get; set; }
        public string expirationDate { get; set; }
        public string startDate { get; set; }
        public bool expirationKnown { get; set; }
        public Vendor vendor { get; set; }
        public Bounties bounties { get; set; }
        public Quests quests { get; set; }
        public bool showNagMessage { get; set; }
        public bool active { get; set; }
    }

    public class Events
    {
        public List<Event> events { get; set; }
    }

    public class Tier
    {
        public long activityHash { get; set; }
        public long specificActivityHash { get; set; }
        public bool isCompleted { get; set; }
        public bool isSuccessful { get; set; }
        public List<int> activeRewardIndexes { get; set; }
        public List<int> skullIndexes { get; set; }
    }

    public class Nightfall
    {
        public int activityBundleHash { get; set; }
        public long specificActivityHash { get; set; }
        public string expirationDate { get; set; }
        public List<Tier> tiers { get; set; }
        public string iconPath { get; set; }
    }

    public class Tier2
    {
        public long activityHash { get; set; }
        public bool isCompleted { get; set; }
        public List<int> activeRewardIndexes { get; set; }
        public List<int> skullIndexes { get; set; }
    }

    public class HeroicStrike
    {
        public long activityBundleHash { get; set; }
        public string expirationDate { get; set; }
        public List<Tier2> tiers { get; set; }
        public string iconPath { get; set; }
        public string image { get; set; }
    }

    public class ActiveRewardIndexes
    {
        //public List<int> __invalid_name__2507671226 { get; set; }
    }

    public class DailyChapter
    {
        public long activityBundleHash { get; set; }
        public string expirationDate { get; set; }
        public bool isCompleted { get; set; }
        public bool isLocked { get; set; }
        public List<long> tierActivityHashes { get; set; }
        public ActiveRewardIndexes activeRewardIndexes { get; set; }
        public string iconPath { get; set; }
    }

    public class DailyCrucible
    {
        public long activityBundleHash { get; set; }
        public string expirationDate { get; set; }
        public bool isCompleted { get; set; }
        public List<object> activeRewardIndexes { get; set; }
        public string iconPath { get; set; }
        public string image { get; set; }
    }

    public class ArtRegions2
    {
    }

    public class Item2
    {
        public object itemHash { get; set; }
        public int bindStatus { get; set; }
        public bool isEquipped { get; set; }
        public string itemInstanceId { get; set; }
        public int itemLevel { get; set; }
        public int stackSize { get; set; }
        public int qualityLevel { get; set; }
        public List<object> stats { get; set; }
        public bool canEquip { get; set; }
        public int equipRequiredLevel { get; set; }
        public int unlockFlagHashRequiredToEquip { get; set; }
        public int cannotEquipReason { get; set; }
        public int damageType { get; set; }
        public int damageTypeHash { get; set; }
        public int damageTypeNodeIndex { get; set; }
        public int damageTypeStepIndex { get; set; }
        public int talentGridHash { get; set; }
        public List<object> nodes { get; set; }
        public bool useCustomDyes { get; set; }
        public ArtRegions2 artRegions { get; set; }
        public bool isEquipment { get; set; }
        public bool isGridComplete { get; set; }
        public List<object> perks { get; set; }
        public int location { get; set; }
        public int transferStatus { get; set; }
        public bool locked { get; set; }
        public bool lockable { get; set; }
        public List<object> objectives { get; set; }
        public int state { get; set; }
    }

    public class Cost2
    {
        public object itemHash { get; set; }
        public int value { get; set; }
    }

    public class Order
    {
        public Item2 item { get; set; }
        public int vendorItemIndex { get; set; }
        public int itemStatus { get; set; }
        public List<Cost2> costs { get; set; }
        public List<int> requiredUnlockFlags { get; set; }
        public List<object> failureIndexes { get; set; }
    }

    public class Stat
    {
        public object statHash { get; set; }
        public int value { get; set; }
        public int maximumValue { get; set; }
    }

    public class PrimaryStat2
    {
        public int statHash { get; set; }
        public int value { get; set; }
        public int maximumValue { get; set; }
    }

    public class Progression2
    {
        public int dailyProgress { get; set; }
        public int weeklyProgress { get; set; }
        public int currentProgress { get; set; }
        public int level { get; set; }
        public int step { get; set; }
        public int progressToNextLevel { get; set; }
        public int nextLevelAt { get; set; }
        public int progressionHash { get; set; }
    }

    public class Node
    {
        public bool isActivated { get; set; }
        public int stepIndex { get; set; }
        public int state { get; set; }
        public bool hidden { get; set; }
        public int nodeHash { get; set; }
    }

    public class ArtRegions3
    {
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

    public class Item3
    {
        public object itemHash { get; set; }
        public int bindStatus { get; set; }
        public bool isEquipped { get; set; }
        public string itemInstanceId { get; set; }
        public int itemLevel { get; set; }
        public int stackSize { get; set; }
        public int qualityLevel { get; set; }
        public List<Stat> stats { get; set; }
        public PrimaryStat2 primaryStat { get; set; }
        public bool canEquip { get; set; }
        public int equipRequiredLevel { get; set; }
        public object unlockFlagHashRequiredToEquip { get; set; }
        public int cannotEquipReason { get; set; }
        public int damageType { get; set; }
        public object damageTypeHash { get; set; }
        public int damageTypeNodeIndex { get; set; }
        public int damageTypeStepIndex { get; set; }
        public Progression2 progression { get; set; }
        public object talentGridHash { get; set; }
        public List<Node> nodes { get; set; }
        public bool useCustomDyes { get; set; }
        public ArtRegions3 artRegions { get; set; }
        public bool isEquipment { get; set; }
        public bool isGridComplete { get; set; }
        public List<object> perks { get; set; }
        public int location { get; set; }
        public int transferStatus { get; set; }
        public bool locked { get; set; }
        public bool lockable { get; set; }
        public List<Objective> objectives { get; set; }
        public int state { get; set; }
    }

    public class TestWeapon
    {
        public Item3 item { get; set; }
        public int vendorItemIndex { get; set; }
        public int itemStatus { get; set; }
        public List<object> costs { get; set; }
        public List<object> failureIndexes { get; set; }
    }

    public class ArmsDay
    {
        public bool active { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string nextStartDate { get; set; }
        public bool expirationKnown { get; set; }
        public bool canPlaceOrder { get; set; }
        public List<Order> orders { get; set; }
        public List<TestWeapon> testWeapons { get; set; }
        public int vendorHash { get; set; }
    }

    public class WeeklyCrucible
    {
        public long activityBundleHash { get; set; }
        public string expirationDate { get; set; }
        public bool isCompleted { get; set; }
        public List<int> activeRewardIndexes { get; set; }
        public string iconPath { get; set; }
        public string image { get; set; }
        public int completionCount { get; set; }
        public int maxCompletions { get; set; }
    }

    public class ArtRegions4
    {
    }

    public class Objective2
    {
        public object objectiveHash { get; set; }
        public int destinationHash { get; set; }
        public int activityHash { get; set; }
        public int progress { get; set; }
        public bool hasProgress { get; set; }
        public bool isComplete { get; set; }
    }

    public class Item4
    {
        public object itemHash { get; set; }
        public int bindStatus { get; set; }
        public bool isEquipped { get; set; }
        public string itemInstanceId { get; set; }
        public int itemLevel { get; set; }
        public int stackSize { get; set; }
        public int qualityLevel { get; set; }
        public List<object> stats { get; set; }
        public bool canEquip { get; set; }
        public int equipRequiredLevel { get; set; }
        public int unlockFlagHashRequiredToEquip { get; set; }
        public int cannotEquipReason { get; set; }
        public int damageType { get; set; }
        public int damageTypeHash { get; set; }
        public int damageTypeNodeIndex { get; set; }
        public int damageTypeStepIndex { get; set; }
        public int talentGridHash { get; set; }
        public List<object> nodes { get; set; }
        public bool useCustomDyes { get; set; }
        public ArtRegions4 artRegions { get; set; }
        public bool isEquipment { get; set; }
        public bool isGridComplete { get; set; }
        public List<object> perks { get; set; }
        public int location { get; set; }
        public int transferStatus { get; set; }
        public bool locked { get; set; }
        public bool lockable { get; set; }
        public List<Objective2> objectives { get; set; }
        public int state { get; set; }
    }

    public class SaleItem2
    {
        public Item4 item { get; set; }
        public int vendorItemIndex { get; set; }
        public int itemStatus { get; set; }
        public List<object> costs { get; set; }
        public List<object> failureIndexes { get; set; }
    }

    //public class __invalid_type__1527174714
    //{
    //    public List<SaleItem2> saleItems { get; set; }
    //}

    public class AvailableBounties
    {
        //public __invalid_type__1527174714 __invalid_name__1527174714 { get; set; }
    }

    public class Data
    {
        public int nightfallActivityHash { get; set; }
        public List<long> heroicStrikeHashes { get; set; }
        public List<long> dailyChapterHashes { get; set; }
        public string nightfallResetDate { get; set; }
        public string heroicStrikeResetDate { get; set; }
        public string dailyChapterResetDate { get; set; }
        public long dailyCrucibleHash { get; set; }
        public string dailyCrucibleResetDate { get; set; }
        public List<int> nightfallRewardIndexes { get; set; }
        public List<object> dailyCrucibleRewardIndexes { get; set; }
        public HeroicStrikeRewardIndexes heroicStrikeRewardIndexes { get; set; }
        public DailyChapterRewardIndexes dailyChapterRewardIndexes { get; set; }
        public List<Arena> arena { get; set; }
        public Events events { get; set; }
        public Nightfall nightfall { get; set; }
        public HeroicStrike heroicStrike { get; set; }
        public DailyChapter dailyChapter { get; set; }
        public DailyCrucible dailyCrucible { get; set; }
        public ArmsDay armsDay { get; set; }
        public List<WeeklyCrucible> weeklyCrucible { get; set; }
        public AvailableBounties availableBounties { get; set; }
    }

    public class Response
    {
        public Data data { get; set; }
    }

    public class MessageData
    {
    }

    public class OlderAdvisorsEndpoint
    {
        public Response Response { get; set; }
        public int ErrorCode { get; set; }
        public int ThrottleSeconds { get; set; }
        public string ErrorStatus { get; set; }
        public string Message { get; set; }
        public MessageData MessageData { get; set; }
    }
}
