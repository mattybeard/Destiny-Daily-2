using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyDAL
{
    public class TrialsManager
    {
        private DestinySqlEntities db { get; set; }
        private DateTime TodayDate => DateTime.Now.AddHours(-9.0).AddMinutes(2);
        private DateTime TrialsDate
        {
            get
            {
                var today = DateTime.Now.AddHours(-9.0).AddMinutes(2);
                while (today.DayOfWeek != DayOfWeek.Friday)
                {
                    today = today.AddDays(-1);
                }

                return today;
            }
        }
        public bool IsActive
        {
            get
            {
                if (TodayDate.DayOfWeek == DayOfWeek.Saturday || TodayDate.DayOfWeek == DayOfWeek.Sunday || TodayDate.DayOfWeek == DayOfWeek.Monday)
                    return true;

                if (TodayDate.DayOfWeek == DayOfWeek.Friday && TodayDate.Hour >= 8)
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
