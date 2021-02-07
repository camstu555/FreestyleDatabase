using System;
using System.Collections.Generic;
using System.Text;

namespace FreestyleDatabase.Shared.Models
{
    public class DataAggregateModel
    {
        public DataAggregateModel()
        {
            MatchesWithVideo = new List<string>();
            MatchesWithForfeits = new List<string>();
            Matches = new List<string>();
            Countries = new List<string>();
            Wrestlers = new List<string>();
            WeightClasses = new List<string>();
            Locations = new List<string>();
        }

        public int TotalMatches { get; set; }

        public int TotalMatchesWithVideo { get; set; }

        public int TotalMatchesWithForfeits { get; set; }

        public int TotalWrestlers { get; set; }

        public int TotalCountries { get; set; }

        public int TotalWeightClasses { get; set; }

        public int TotalLocations { get; set; }

        public DateTimeOffset? EarliestMatchDate { get; set; }

        public string EarliestMatchId { get; set; }

        public string EarliestMatchTitle { get; set; }

        public string EarliestMatchWrestler1Name { get; set; }
        
        public string EarliestMatchWrestler1Image { get; set; }

        public string EarliestMatchWrestler2Name { get; set; }
        
        public string EarliestMatchWrestler2Image { get; set; }

        public DateTimeOffset? MostRecentMatchDate { get; set; }

        public string MostRecentMatchId { get; set; }

        public string MostRecentMatchTitle { get; set; }

        public string MostRecentMatchWrestler1Name { get; set; }
        
        public string MostRecentMatchWrestler1Image { get; set; }

        public string MostRecentMatchWrestler2Name { get; set; }
        
        public string MostRecentMatchWrestler2Image { get; set; }

        public List<string> Wrestlers { get; private  set; }

        public List<string> MatchesWithVideo { get; private set; }

        public List<string> MatchesWithForfeits { get; private set; }

        public List<string> Matches { get; private set; }


        public List<string> Countries { get; private set; }
        

        public List<string> WeightClasses { get; private set; }
        

        public List<string> Locations { get; private set; }
        
    }
}
