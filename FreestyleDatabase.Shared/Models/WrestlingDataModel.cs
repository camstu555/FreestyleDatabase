using System;
using System.Globalization;

namespace FreestyleDatabase.Shared.Models
{
    public class WrestlingDataModel
    {
        public string Href
        {
            get
            {
                return $"https://freestyledb.azurewebsites.net/api/FreeStyleLookup?id={Id}";
            }
        }

        public string Id { get; set; }

        public string Country1 { get; set; }

        public string WeightClass { get; set; }

        public string WrestlerName1 { get; set; }

        public string WrestlerImage1 { get; set; }

        public string Result { get; set; }

        public string Score { get; set; }

        public int WreslterName1Score { get; set; }

        public int WreslterName2Score { get; set; }

        public string Country2 { get; set; }

        public string WrestlerName2 { get; set; }

        public string WrestlerImage2 { get; set; }

        public string Result2 { get; set; }

        public DateTimeOffset? Date { get; set; }

        public string Venue { get; set; }

        public string Location { get; set; }

        public string Round { get; set; }

        public string Video { get; set; }

        public string Brackets { get; set; }

        public string Country1Emoji { get; set; }

        public string FullCountryName1 { get; set; }

        public string FullCountryName2 { get; set; }

        public string Country2Emoji { get; set; }

        public string WreslterId1 { get; set; }

        public string WreslterId2 { get; set; }
    }
}