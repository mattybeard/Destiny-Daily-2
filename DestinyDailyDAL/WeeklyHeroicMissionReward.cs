//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DestinyDailyDAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class WeeklyHeroicMissionReward
    {
        public int id { get; set; }
        public Nullable<int> weeklyheroicid { get; set; }
        public Nullable<long> rewardid { get; set; }
    
        public virtual InventoryItem inventoryitem { get; set; }
        public virtual WeeklyHeroic weeklyheroic { get; set; }
    }
}
