using System;
using System.Collections.Generic;

namespace FreestyleDatabase.Shared.Models
{
    public class WrestlingAggregatesModel
    {
        public WrestlingAggregatesModel()
        {
            GoldMedalMatches = new List<string>();
            SilverMedalMatches = new List<string>();
            BronzeMedalMatches = new List<string>();
            MostRecentMatches = new SearchCollectionResponseModel<WrestlingDataModel>();
        }

        public string WrestlerImageUrl { get; set; }

        public string WrestlerName { get; set; }

        public string WrestlerId { get; set; }

        /// <summary>
        /// count of WrestlerName1
        /// </summary>
        public int Wins { get; set; }

        /// <summary>
        /// count of WrestlerName2
        /// </summary>
        public int Losses { get; set; }

        /// <summary>
        /// returns WrestlerName1 and Round = Gold
        /// </summary>
        public List<string> GoldMedalMatches { get; private set; }

        /// <summary>
        /// returns WrestlerName2 and Round = Gold
        /// </summary>
        public List<string> SilverMedalMatches { get; private set; }

        /// <summary>
        /// returns WrestlerName1 and Round = Bronze
        /// </summary>
        public List<string> BronzeMedalMatches { get; private set; }

        /// <summary>
        /// returns count of WrestlerName1 and result = VFA
        /// </summary>
        public int Pins { get; set; }

        /// <summary>
        /// returns count of WrestlerName1 and result = VSU or VSU1
        /// </summary>
        public int Techs { get; set; }

        /// <summary>
        /// returns count of WrestlerName1 and result = VPO or VPO1
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// if (WrestlerName1 = search) add left of hyphen
        /// if (WrestlerName2 = search) add right of hyphen
        /// divide by count of total matches for that wrestler
        /// </summary>
        public double AverageOffensivePointsPerMatch { get; set; }

        public double BonusRate { get; set; }

        /// <summary>
        /// if (WrestlerName1 = search) add right of hyphen
        /// if (WrestlerName2 = search) add left of hyphen
        /// divide by count of total matches for that wrestler
        /// </summary>
        public double AverageDefensivePointsPerMatch { get; set; }

        /// <summary>
        ///  returns most recent 10 matches where WrestlerName1 or WrestlerName2 = search
        /// </summary>
        public SearchCollectionResponseModel<WrestlingDataModel> MostRecentMatches { get; private set; }

        public string WrestlerWeight { get; set; }

        public string WrestlerCountry { get; set; }

        public string WrestlerCountryEmoji { get; set; }

        public DateTimeOffset? OldestMatchDate { get; set; }
    }
}