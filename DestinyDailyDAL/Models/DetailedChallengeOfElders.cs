﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestinyDailyDAL.Models
{
    public class DetailedChallengeOfElders
    {
        public ChallengeOfElder Challenge { get; set; }
        public List<BountyDay> Bounties { get; set; }
    }
}
