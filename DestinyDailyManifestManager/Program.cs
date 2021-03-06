﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Validation;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinyDailyDAL;
using DestinyDailyApiManager;
using DestinyDailyApiManager.Models.D2.Manifest;
using DestinyDailyApiManager.Models.Manifest.Activity;
using DestinyDailyDAL.Destiny1;
using DestinyDailyDAL.Destiny2;
using Newtonsoft.Json;

namespace DestinyDailyManifestManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var manifestUrl = BungieApi.GetManifestUrl();

            var dbConnection = new SQLiteConnection($"Data Source={args[0]};Version=3;");
            dbConnection.Open();

            //var db = new DestinySqlEntities();

            //Console.WriteLine("Processing Combatants");
            //ProcessCombantants(dbConnection, db);

            //Console.WriteLine("Processing Activities");
            //ProcessManifestActivities(dbConnection, db);

            //Console.WriteLine("Processing Items");
            //ProcessInventoryItems(dbConnection, db);

            var db2 = new DestinyDaily2Entities();
            Console.WriteLine("Processing Emblems");
            ProcessInventoryItems(dbConnection, db2, "Emblem");

            Console.WriteLine("Processing Sparrows");
            ProcessInventoryItems(dbConnection, db2, "Sparrow");

            Console.WriteLine("Processing Ships");
            ProcessInventoryItems(dbConnection, db2, "Ship");

            Console.WriteLine("Finished");
            Console.ReadLine();
        }

        private static void ProcessCombantants(SQLiteConnection dbConnection, DestinySqlEntities db)
        {
            var sql = "SELECT quote(json) FROM DestinyCombatantDefinition";
            var command = new SQLiteCommand(sql, dbConnection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var jsonStripped = ((string)reader["quote(json)"]).Replace("'{", "{").Replace("}'", "}");
                var item = JsonConvert.DeserializeObject<DestinyDailyApiManager.Models.Manifest.Combatant.Combatant>(jsonStripped);

                var mappedItem = db.Combatants.FirstOrDefault(c => c.id == item.combatantHash);
                if (mappedItem == null)
                {
                    mappedItem = new Combatant()
                    {
                        id = item.combatantHash,
                        name = item.combatantName,
                        desc = item.description,
                        icon = item.icon,
                        image = item.image
                    };
                    db.Combatants.Add(mappedItem);
                }
                else
                {
                    mappedItem.id = item.combatantHash;
                    mappedItem.name = item.combatantName;
                    mappedItem.desc = item.description;
                    mappedItem.icon = item.icon;
                    mappedItem.image = item.image;
                }
            }
            try { db.SaveChanges(); }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
            }
        }

        private static void ProcessInventoryItems(SQLiteConnection dbConnection, DestinyDaily2Entities db, string type)
        {
            var added = 0;
            var existing = 0;

            var sql = "SELECT quote(json) FROM DestinyInventoryItemDefinition";

            if (type == "Emblem")
            {
                sql = $"{sql} WHERE quote(json) LIKE '%Emblem%';";
            }
            else if (type == "Sparrow")
            {
                sql = $"{sql} WHERE quote(json) LIKE '%\"itemTypeDisplayName\":\"Vehicle\"%';";
            }
            else if (type == "Ship")
            {
                sql = $"{sql} WHERE quote(json) LIKE '%\"itemTypeDisplayName\":\"Ship\"%';";
            }

            var command = new SQLiteCommand(sql, dbConnection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var jsonStripped = ((string) reader["quote(json)"]).Replace("'{", "{").Replace("}'", "}");
                var inventoryItem = JsonConvert.DeserializeObject<InventoryItemDefinition>(jsonStripped);

                if (type == "Emblem")
                {
                    var mappedItem = db.InventoryEmblems.FirstOrDefault(i => i.id == inventoryItem.hash);
                    if (mappedItem == null)
                    {
                        mappedItem = new InventoryEmblem()
                        {
                            id = inventoryItem.hash,
                            name = inventoryItem.displayProperties.name.Replace("''","'"),
                            icon = inventoryItem.displayProperties.icon,
                            secondaryIcon = inventoryItem.secondaryIcon,
                            secondaryOverlay = inventoryItem.secondaryOverlay,
                            secondarySpecial = inventoryItem.secondarySpecial,
                            screenshot = inventoryItem.screenshot,
                            rarity = inventoryItem.inventory.tierTypeName
                        };

                        db.InventoryEmblems.Add(mappedItem);
                        added++;

                        if (added%500 == 0)
                        {
                            Console.WriteLine($"Saving first {added} records");
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        mappedItem.id = inventoryItem.hash;
                        mappedItem.name = inventoryItem.displayProperties.name.Replace("''", "'");
                        mappedItem.icon = inventoryItem.displayProperties.icon;
                        mappedItem.secondaryIcon = inventoryItem.secondaryIcon;
                        mappedItem.secondaryOverlay = inventoryItem.secondaryOverlay;
                        mappedItem.secondarySpecial = inventoryItem.secondarySpecial;
                        mappedItem.screenshot = inventoryItem.screenshot;
                        mappedItem.rarity = inventoryItem.inventory.tierTypeName;

                        existing++;
                    }
                }

                if (type == "Sparrow")
                {
                    var mappedItem = db.InventorySparrows.FirstOrDefault(i => i.id == inventoryItem.hash);
                    if (mappedItem == null)
                    {
                        mappedItem = new InventorySparrow()
                        {
                            id = inventoryItem.hash,
                            name = inventoryItem.displayProperties.name.Replace("''", "'"),
                            description = inventoryItem.displayProperties.description.Replace("''", "'"),
                            icon = inventoryItem.displayProperties.icon,
                            screenshot = inventoryItem.screenshot,
                            rarity = inventoryItem.inventory.tierTypeName
                        };

                        db.InventorySparrows.Add(mappedItem);
                        added++;

                        if (added % 500 == 0)
                        {
                            Console.WriteLine($"Saving first {added} records");
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        mappedItem.id = inventoryItem.hash;
                        mappedItem.name = inventoryItem.displayProperties.name.Replace("''", "'");
                        mappedItem.description = inventoryItem.displayProperties.description.Replace("''", "'");
                        mappedItem.icon = inventoryItem.displayProperties.icon;
                        mappedItem.screenshot = inventoryItem.screenshot;
                        mappedItem.rarity = inventoryItem.inventory.tierTypeName;

                        existing++;
                    }
                }

                if (type == "Ship")
                {
                    var mappedItem = db.InventoryShips.FirstOrDefault(i => i.id == inventoryItem.hash);
                    if (mappedItem == null)
                    {
                        mappedItem = new InventoryShip()
                        {
                            id = inventoryItem.hash,
                            name = inventoryItem.displayProperties.name.Replace("''", "'"),
                            description = inventoryItem.displayProperties.description.Replace("''", "'"),
                            icon = inventoryItem.displayProperties.icon,
                            screenshot = inventoryItem.screenshot,
                            rarity = inventoryItem.inventory.tierTypeName
                        };

                        db.InventoryShips.Add(mappedItem);
                        added++;

                        if (added % 500 == 0)
                        {
                            Console.WriteLine($"Saving first {added} records");
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        mappedItem.id = inventoryItem.hash;
                        mappedItem.name = inventoryItem.displayProperties.name.Replace("''", "'");
                        mappedItem.description = inventoryItem.displayProperties.description.Replace("''", "'");
                        mappedItem.icon = inventoryItem.displayProperties.icon;
                        mappedItem.screenshot = inventoryItem.screenshot;
                        mappedItem.rarity = inventoryItem.inventory.tierTypeName;

                        existing++;
                    }
                }

            }
            try { db.SaveChanges(); }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
            }
            Console.WriteLine($"{added} Added & {existing} Updated");
        }

        private static void ProcessManifestActivities(SQLiteConnection dbConnection, DestinySqlEntities db)
        {
            var sql = "SELECT quote(json) FROM DestinyActivityDefinition";
            var command = new SQLiteCommand(sql, dbConnection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var jsonStripped = ((string)reader["quote(json)"]).Replace("'{", "{").Replace("}'", "}");
                var item = JsonConvert.DeserializeObject<ActivityDefinition>(jsonStripped);

                var mappedItem = db.ManifestActivities.FirstOrDefault(c => c.id == item.activityHash);
                if (mappedItem == null)
                {
                    mappedItem = new ManifestActivity()
                    {
                        id = item.activityHash,
                        desc = item.activityDescription,
                        icon = item.icon,
                        name = item.activityName,
                        pgcricon = item.pgcrImage
                    };
                    db.ManifestActivities.Add(mappedItem);

                    if (item.skulls != null && item.skulls.Any())
                    {
                        foreach (var skull in item.skulls)
                        {
                            var newSkull = new ManifestSkull()
                            {
                                activityhash = item.activityHash,
                                name = skull.displayName
                            };

                            db.ManifestSkulls.Add(newSkull);
                        }
                    }

                    if (item.rewards != null && item.rewards.Any())
                    {
                        foreach (var rewardList in item.rewards)
                        {
                            foreach (var reward in rewardList.rewardItems)
                            {
                                var newReward = new ManifestReward()
                                {
                                    activityhash = item.activityHash,
                                    rewardHash = reward.itemHash,
                                    value = reward.value
                                };

                                db.ManifestRewards.Add(newReward);
                            }                            
                        }
                    }
                }
                else
                {
                    mappedItem.id = item.activityHash;
                    mappedItem.desc = item.activityDescription;
                    mappedItem.icon = item.icon;
                    mappedItem.name = item.activityName;
                    mappedItem.pgcricon = item.pgcrImage;
                }
            }
            try { db.SaveChanges(); }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                    }
                }
            }
        }
    }
}
