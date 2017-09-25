using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyApiManager.Models.D2.Manifest
{
    public class EquippingBlock
    {
        public long uniqueLabelHash { get; set; }
        public long equipmentSlotTypeHash { get; set; }
        public int attributes { get; set; }
        public long equippingSoundHash { get; set; }
        public long hornSoundHash { get; set; }
        public List<object> displayStrings { get; set; }
    }
}
