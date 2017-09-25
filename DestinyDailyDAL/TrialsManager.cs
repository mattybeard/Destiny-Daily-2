using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinyDailyDAL.Destiny1;

namespace DestinyDailyDAL
{
    public class TrialsManager : DestinyDailyManager
    {
        private DestinySqlEntities db { get; set; }
        public bool IsActive
        {
            get
            {
                if (TodayDate.DayOfWeek == DayOfWeek.Saturday || TodayDate.DayOfWeek == DayOfWeek.Sunday || TodayDate.DayOfWeek == DayOfWeek.Monday)
                    return true;

                if (TodayDate.DayOfWeek == DayOfWeek.Friday && TodayDate.Hour >= 17)
                    return true;

                return false;
            }
        }

        public TrialsManager()
        {
            db = new DestinySqlEntities();
        }

        public TrialsMapDay GetCurrentMap()
        {
            if (!IsActive)
                return null;

            var map = db.TrialsMapDays.FirstOrDefault(d => d.day == TrialsDate.Day && d.month == TrialsDate.Month && d.year == TrialsDate.Year);
            if (map == null)
            {
                GetLatestMap();
                var updatedMap = db.TrialsMapDays.FirstOrDefault(d => d.day == TrialsDate.Day && d.month == TrialsDate.Month && d.year == TrialsDate.Year);

                return updatedMap;
            }

            return map;
        }

        private void GetLatestMap()
        {
            var vendorInformation = DestinyDailyApiManager.BungieApi.GetAdvisors();
            if (vendorInformation.ErrorCode > 1)
                return;

            var latestMap = vendorInformation.Response.data.activities.trials.display;
            var map = new TrialsMapDay()
            {
                map = latestMap.flavor,
                mapimage = latestMap.image,
                day = TrialsDate.Day,
                month = TrialsDate.Month,
                year = TrialsDate.Year
            };
            db.TrialsMapDays.Add(map);
            db.SaveChanges();
        }
    }
}
