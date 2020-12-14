using System;
using System.Collections.Generic;
using System.Text;

namespace FreestyleDatabase.Shared.Models
{
    public class WrestlingAggregatesModel
    {
        public WrestlingAggregatesModel()
        {
            MostRecentMatches = new SearchCollectionResponseModel<WrestlingDataModel>();
        }

        public string WrestlerImageUrl { get; set; }

        public string WrestlerName { get; set; }

        public string WrestlerId { get; set; }

        public int Wins { get; set; }
        //count of WrestlerName1

        public int Losses { get; set; }
        //count of WrestlerName2

        public List<string> GoldMedalMatches { get; set; }
        //returns WrestlerName1 and Round = Gold

        public List<string> SilverMedalMatches { get; set; }
        //returns WrestlerName2 and Round = Gold

        public List<string> BronzeMedalMatches { get; set; }
        //returns WrestlerName1 and Round = Bronze

        public int Pins { get; set; }
        //returns count of WrestlerName1 and result = VFA

        public int Techs { get; set; }
        //returns count of WrestlerName1 and result = VSU or VSU1

        public int Points { get; set; }
        //returns count of WrestlerName1 and result = VPO or VPO1

        public int AverageOffensivePointsPerMatch { get; set; }
        //if (WrestlerName1 = search) add left of hyphen
        //if (WrestlerName2 = search) add right of hyphen
        //divide by count of total matches for that wrestler

        public int AverageDefensivePointsPerMatch { get; set; }
        //if (WrestlerName1 = search) add right of hyphen
        //if (WrestlerName2 = search) add left of hyphen
        //divide by count of total matches for that wrestler

        public SearchCollectionResponseModel<WrestlingDataModel> MostRecentMatches { get; private set; }
        //returns most recent 10 matches where WrestlerName1 or WrestlerName2 = search
    }
}