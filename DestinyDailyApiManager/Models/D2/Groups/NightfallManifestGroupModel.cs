using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DestinyDailyApiManager.Models.D2.API;

namespace DestinyDailyApiManager.Models.D2.Groups
{
    public class NightfallManifestGroupModel
    {
        public NightfallManifestGroupModel()
        {
            Modifiers = new List<ModifierDefinition>();
            Challenges = new List<ModifierDefinition>();
        }
        public ActivityDefinition Activity { get; set; }
        public List<ModifierDefinition> Modifiers { get; set; }
        public List<ModifierDefinition> Challenges { get; set; }
    }
}
