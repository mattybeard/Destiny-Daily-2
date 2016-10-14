using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DestinyDailyDAL;

namespace DestinyDaily2.Models
{
    public class NightfallWeeklyModel
    {
        public DateTime ThisDate { get; set; }
        public Nightfall ThisNightfall { get; set; }
        public WeeklyHeroic ThisWeekly { get; set; }
    }
}