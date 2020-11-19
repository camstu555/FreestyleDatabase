using System;

namespace FreestyleDatabase.Shared.Models
{
    public class WrestlingDataModel
    {
        public string Id { get; set; }

        public string Country1 { get; set; }

        public string WeightClass { get; set; }

        public string WrestlerName1 { get; set; }

        public string Result { get; set; }

        public string Score { get; set; }

        public string Country2 { get; set; }

        public string WrestlerName2 { get; set; }

        public string Result2 { get; set; }

        public DateTime? Date { get; set; }

        public string Venue { get; set; }

        public string Location { get; set; }

        public string Round { get; set; }

        public string Video { get; set; }

        public string Brackets { get; set; }

        public string FixedWrestlerName2 { get; set; }

        public string Country1Emoji { get; set; }

        public string FullCountryName1 { get; set; }

        public string FullCountryName2 { get; set; }

        public string FixedWrestlerName1 { get; set; }

        public string Country2Emoji { get; set; }
    }
}