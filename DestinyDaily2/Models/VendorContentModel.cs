using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DestinyDailyDAL;

namespace DestinyDaily2.Models
{
    public class VendorContentModel
    {
        public List<XurDay> XurSales { get; set; }
        public bool XurInTower { get; set; }
    }
}