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
    
    public partial class WeeklyHeroic
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WeeklyHeroic()
        {
            this.Modifiers = new HashSet<WeeklyHeroicModifier>();
            this.Rewards = new HashSet<WeeklyHeroicMissionReward>();
        }
    
        public int id { get; set; }
        public Nullable<long> missionid { get; set; }
        public Nullable<int> day { get; set; }
        public Nullable<int> month { get; set; }
        public Nullable<int> year { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WeeklyHeroicModifier> Modifiers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WeeklyHeroicMissionReward> Rewards { get; set; }
        public virtual ManifestActivity ManifestActivity { get; set; }
    }
}
