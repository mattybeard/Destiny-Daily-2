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
    
    public partial class CrucibleWeeklyDay
    {
        public int id { get; set; }
        public Nullable<long> activityid { get; set; }
        public Nullable<int> day { get; set; }
        public Nullable<int> month { get; set; }
        public Nullable<int> year { get; set; }
    
        public virtual ManifestActivity ManifestActivity { get; set; }
    }
}
