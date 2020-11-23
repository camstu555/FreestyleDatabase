using System;
using System.Collections.Generic;
using System.Text;

namespace FreestyleDatabase.Shared.Models
{
    public class WrestlingAggregatesModel
    {
        public int Wins { get; set; }

        public int Losses { get; set; }

        public List<string> GoldMedalMatches { get; set; }

        public List<string> SilverMedalMatches { get; set; }

        public List<string> BronzeMedalMatches { get; set; }

        public int Pins { get; set; }

        public int Techs { get; set; }

        public int Points { get; set; }

        public int AverageOffensivePointsPerMatch { get; set; }

        public int AverageDefensivePointsPerMatch { get; set; }

        public List<string> MostRecentMatches { get; set; }
    }
}