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
    
    public partial class XurLocationDay
    {
        public int id { get; set; }
        public int xurlocationid { get; set; }
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    
        public virtual XurLocation xurlocation { get; set; }
    }
}