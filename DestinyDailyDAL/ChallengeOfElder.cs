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
    
    public partial class ChallengeOfElder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChallengeOfElder()
        {
            this.challengeofeldersrounds = new HashSet<ChallengeOfEldersRound>();
            this.challengeofeldersmodifiers = new HashSet<ChallengeOfEldersModifier>();
            this.challengeofeldersscoremodifiers = new HashSet<ChallengeOfEldersScoreModifier>();
        }
    
        public int id { get; set; }
        public Nullable<long> missionid { get; set; }
        public Nullable<int> day { get; set; }
        public Nullable<int> month { get; set; }
        public Nullable<int> year { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChallengeOfEldersRound> challengeofeldersrounds { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChallengeOfEldersModifier> challengeofeldersmodifiers { get; set; }
        public virtual ManifestActivity manifestactivity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChallengeOfEldersScoreModifier> challengeofeldersscoremodifiers { get; set; }
    }
}
