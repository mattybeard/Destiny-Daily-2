using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyApiManager.Models.D2.API
{
    public class Quality
    {
        public List<object> itemLevels { get; set; }
        public int qualityLevel { get; set; }
        public string infusionCategoryName { get; set; }
        public long infusionCategoryHash { get; set; }
        public long progressionLevelRequirementHash { get; set; }
    }
}
