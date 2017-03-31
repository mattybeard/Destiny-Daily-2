using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyDAL
{
    public class XurManager
    {
        private DestinySqlEntities db { get; set; }
        private DateTime TodayDate => DateTime.Now.AddHours(-9.0).AddMinutes(5);
        private DateTime XurDate
        {
            get
            {
                var today = DateTime.Now.AddHours(-9.0).AddMinutes(5);
                while (today.DayOfWeek != DayOfWeek.Friday)
                {
                    today = today.AddDays(-1);
                }

                return today;
            }
        }
        public bool InTower
        {
            get
            {
                if (TodayDate.DayOfWeek == DayOfWeek.Friday || TodayDate.DayOfWeek == DayOfWeek.Saturday)
                    return true;

                return false;
            }
        }

        public XurManager()
        {
            db = new DestinySqlEntities();
        }

        public List<XurDay> GetCurrentItems()
        {
            if(!InTower)
                return new List<XurDay>();

            var items = db.XurDays.Where(d => d.day == XurDate.Day && d.month == XurDate.Month && d.year == XurDate.Year).ToList();
            if (!items.Any())
            {
                CreateItems();
                var updatedItems = db.XurDays.Where(d => d.day == XurDate.Day && d.month == XurDate.Month && d.year == XurDate.Year).ToList();
                return updatedItems;
            }

            return items;
        }

        public bool IsActive()
        {
            if (!InTower)
                return false;

            return HasCurrentItems();
        }

        private bool HasCurrentItems()
        {
            return GetCurrentItems().Any();
        }

        private void CreateItems()
        {
            var vendorInformation = DestinyDailyApiManager.BungieApi.GetOldAdvisors();
            if (vendorInformation.ErrorCode > 1)
                return;

            var xurEvent = vendorInformation.Response.data.events.events.FirstOrDefault(e => e.eventIdentifier == "SPECIAL_EVENT_BLACK_MARKET");
            if (xurEvent?.vendor?.saleItemCategories != null)
            {
                var group = 1;
                foreach (var cat in xurEvent.vendor.saleItemCategories)
                {
                    foreach (var item in cat.saleItems)
                    {
                        var entry = new XurDay()
                        {
                            day = XurDate.Day,
                            month = XurDate.Month,
                            year = XurDate.Year,
                            gearid = item.item.itemHash,
                            group = group
                        };

                        db.XurDays.Add(entry);
                    }
                    group++;
                }
                db.SaveChanges();
            }
        }

        public XurLocationDay GetCurrentLocation()
        {
            var location = db.XurLocationDays.FirstOrDefault(d => d.day == XurDate.Day && d.month == XurDate.Month && d.year == XurDate.Year);

            return location;
        }
    }
}
