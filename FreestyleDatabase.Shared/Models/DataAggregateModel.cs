using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

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

        [JsonIgnore]
        public List<string> Matches { get; private set; }

        public int TotalMatchesWithVideo { get; set; }

        [JsonIgnore]
        public List<string> MatchesWithVideo { get; private set; }

        public int TotalMatchesWithForfeits { get; set; }

        [JsonIgnore]
        public List<string> MatchesWithForfeits { get; private set; }

        public int TotalWrestlers { get; set; }

        [JsonIgnore]
        public List<string> Wrestlers { get; private  set; }

        public int TotalCountries { get; set; }

        public List<string> Countries { get; private set; }

        public DateTimeOffset? EarliestMatchDate { get; set; }

        public DateTimeOffset? MostRecentMatchDate { get; set; }
    }
}
