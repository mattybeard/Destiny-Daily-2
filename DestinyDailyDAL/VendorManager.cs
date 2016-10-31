﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyDAL
{
    public class VendorManager
    {
        private DestinySqlEntities db { get; set; }

        public VendorManager()
        {
            db = new DestinySqlEntities();
        }

        public List<MaterialExchange> GetMaterialExchange(DateTime standardDate)
        {
            var exchanges = db.MaterialExchanges.Where(d => d.day == standardDate.Day && d.month == standardDate.Month && d.year == standardDate.Year);
            if (!exchanges.Any())
            {
                CreateMaterialExchanges(standardDate);
                var updatedExchanges = db.MaterialExchanges.Where(d => d.day == standardDate.Day && d.month == standardDate.Month && d.year == standardDate.Year);
                return updatedExchanges.ToList();
            }

            return exchanges.ToList();
        }

        private void CreateMaterialExchanges(DateTime standardDate)
        {
            var hiddenHashes = CreateStopHashes();
            var vendorKey = new Dictionary<string, string>
            {
                {"Future War Cult", "1821699360"}, {"Dead Orbit", "3611686524"}, {"New Monarchy", "1808244981"}, {"Eris Morn", "174528503"}
            };

            foreach (var faction in vendorKey)
            {
                var factionDetails = DestinyDailyApiManager.BungieApi.GetVendorMetaData(faction.Value);
                var factionItems = factionDetails.Response.data.vendor.saleItemCategories.FirstOrDefault(c => c.categoryTitle == "Material Exchange" || c.categoryTitle == "Crota's Bane Rank 2");
                if (factionItems != null)
                {
                    var customExchange = factionItems.saleItems.FirstOrDefault(c => !hiddenHashes.Contains(c.item.itemHash));
                    if (customExchange != null)
                    {
                        var item = db.InventoryItems.FirstOrDefault(c => c.id == customExchange.item.itemHash);
                        if (item != null)
                            CreateMaterialExchange(standardDate, faction.Key, item.name);
                    }
                }
            }

            db.SaveChanges();
        }

        private List<long> CreateStopHashes()
        {
            var blackList = new List<long>();
            blackList.Add(2709988006);
            blackList.Add(1298784603);
            blackList.Add(1590258561);
            blackList.Add(2698915936);
            blackList.Add(279670352);
            blackList.Add(3157586064);
            blackList.Add(3465849241);
            blackList.Add(246318587);
            blackList.Add(2873148414);
            blackList.Add(3069074202);
            blackList.Add(4065259641);
            blackList.Add(3975516730);
            blackList.Add(1989520068);
            blackList.Add(807600833);
            blackList.Add(3722494237);
            return blackList;
        }

        private void CreateMaterialExchange(DateTime standardDate, string vendorName, string name)
        {
            var materialExchange = new MaterialExchange()
            {
                day = standardDate.Day,
                month = standardDate.Month,
                year = standardDate.Year,
                material = name,
                vendor = vendorName
            };

            db.MaterialExchanges.Add(materialExchange);
        }
    }
}