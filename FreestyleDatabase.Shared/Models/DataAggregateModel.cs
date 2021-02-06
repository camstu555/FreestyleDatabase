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
        }

        public int TotalMatches { get; set; }

        public List<string> Matches { get; private set; }

        public int TotalMatchesWithVideo { get; set; }

        public List<string> MatchesWithVideo { get; private set; }

        public int TotalMatchesWithForfeits { get; set; }

        public List<string> MatchesWithForfeits { get; private set; }

        public int TotalWrestlers { get; set; }

        public List<string> Wrestlers { get; private  set; }

        public int TotalCountries { get; set; }

        public List<string> Countries { get; private set; }

        public DateTimeOffset? EarliestMatchDate { get; set; }

        public DateTimeOffset? MostRecentMatchDate { get; set; }
    }
}
