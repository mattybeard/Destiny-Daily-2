using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyDAL
{
    public class XurManager
    {
        private DestinyEntities db { get; set; }

        public XurManager()
        {
            db = new DestinyEntities();
        }

        public List<XurDay> GetItems(DateTime standardDate)
        {
            var items = db.XurDays.Where(d => d.day == standardDate.Day && d.month == standardDate.Month && d.year == standardDate.Year).ToList();
            if (!items.Any())
            {
                CreateItems(standardDate);
                var updatedItems = db.XurDays.Where(d => d.day == standardDate.Day && d.month == standardDate.Month && d.year == standardDate.Year).ToList();
                return updatedItems;
            }

            return items;
        }

        private void CreateItems(DateTime standardDate)
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
                            day = standardDate.Day,
                            month = standardDate.Month,
                            year = standardDate.Year,
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
    }
}
