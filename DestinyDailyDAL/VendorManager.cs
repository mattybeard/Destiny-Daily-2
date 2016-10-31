using System;
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


    }
}
