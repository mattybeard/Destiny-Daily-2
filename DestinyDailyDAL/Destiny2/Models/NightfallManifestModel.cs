using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyDAL.Destiny2.Models
{
    public class NightfallManifestModel
    {
        public NightfallManifestModel()
        {
            Challenges = new List<UnclassifiedChallenge>();
        }

        public long? Hash { get; set; }
        public string Name { get; set; }
        public string PgcrIcon { get; set; }
        public List<UnclassifiedModifier> Modifiers { get; set; }
        public List<UnclassifiedChallenge> Challenges { get; set; }
    }
}
