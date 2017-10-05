using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinyDailyApiManager.Models.D2.Manifest;
using DestinyDailyDAL.Destiny1;
using DestinyDailyDAL.Destiny2;

namespace DestinyDailyDAL
{
    public class XurManager : DestinyDailyManager
    {
        private DestinySqlEntities db { get; set; }
        private DestinyDaily2Entities db2 { get; set; }
        public bool InTower(int destinyVersion)
        {
            {
                if (TodayDate.DayOfWeek == DayOfWeek.Friday || TodayDate.DayOfWeek == DayOfWeek.Saturday || ((TodayDate.DayOfWeek == DayOfWeek.Sunday || TodayDate.DayOfWeek == DayOfWeek.Monday) && destinyVersion == 2))
                    return true;

                return false;
            }
        }

        public XurManager()
        {
            db = new DestinySqlEntities();
            db2 = new DestinyDaily2Entities();
        }

        public List<InventoryItemDefinition> GetD2CurrentItems()
        {
            var output = new List<InventoryItemDefinition>();

            if (!InTower(2))
                return output;

            var items = db2.XurDays.Where(d => d.day == XurDate.Day && d.month == XurDate.Month && d.year == XurDate.Year).ToList();

            if (items.Any())
            {
                
                var itemsManifest = DestinyDailyApiManager.BungieApi.GetPlumbing<Dictionary<long, InventoryItemDefinition>>("DestinyInventoryItemDefinition");

                foreach (var item in items.Where(i => i.itemid != null))
                {
                    InventoryItemDefinition existingItem;
                    itemsManifest.TryGetValue(item.itemid.Value, out existingItem);

                    if(existingItem != null)
                        output.Add(existingItem);
                }
            }

            return output;
        }

        public List<XurD1Day> GetD1CurrentItems()
        {
            if(!InTower(1))
                return new List<XurD1Day>();

            var items = db.XurD1Days.Where(d => d.day == XurDate.Day && d.month == XurDate.Month && d.year == XurDate.Year).ToList();
            if (!items.Any())
            {
                CreateD1Items();
                var updatedItems = db.XurD1Days.Where(d => d.day == XurDate.Day && d.month == XurDate.Month && d.year == XurDate.Year).ToList();
                foreach (var item in updatedItems)
                    item.InventoryItem = db.InventoryItems.FirstOrDefault(i => i.id == item.gearid);

                return updatedItems;
            }

            return items;
        }

        public bool IsActive(int destinyVersion)
        {
            if (!InTower(destinyVersion))
                return false;

            return HasCurrentItems(destinyVersion);
        }

        private bool HasCurrentItems(int destinyVersion)
        {
            if(destinyVersion == 1)
                return GetD1CurrentItems().Any();

            return GetD2CurrentItems().Any();
        }

        private void CreateD1Items()
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
                        var entry = new XurD1Day()
                        {
                            day = XurDate.Day,
                            month = XurDate.Month,
                            year = XurDate.Year,
                            gearid = item.item.itemHash,
                            group = group
                        };

                        db.XurD1Days.Add(entry);
                    }
                    group++;
                }
                db.SaveChanges();
            }
        }

        public XurLocationDay GetD2CurrentLocation()
        {
            var location = db2.XurLocationDays.FirstOrDefault(d => d.day == XurDate.Day && d.month == XurDate.Month && d.year == XurDate.Year);

            return location;
        }

        public XurD1LocationDay GetD1CurrentLocation()
        {
            var location = db.XurD1LocationDays.FirstOrDefault(d => d.day == XurDate.Day && d.month == XurDate.Month && d.year == XurDate.Year);

            return location;
        }
    }
}
