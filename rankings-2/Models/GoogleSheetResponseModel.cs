using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace rankings2.Models
{
    public class GoogleSheetResponseModel
    {
        [JsonPropertyName("feed")]
        public GoogleSheetFeedModel Feed { get; set; }

    }

    public class GoogleSheetFeedModel
    {
        [JsonPropertyName("openSearch$totalResults")]
        public GoogleSheetValueModel OpenSearchtotalResults { get; set; }

        [JsonPropertyName("openSearch$startIndex")]
        public GoogleSheetValueModel OpenSearchstartIndex { get; set; }

        [JsonPropertyName("entry")]
        public List<GoogleSheetEntryModel> Entry { get; set; }

    }

    public class GoogleSheetEntryModel
    {
        [JsonPropertyName("title")]
        public GoogleSheetValueModel Title { get; set; }

        [JsonPropertyName("content")]
        public GoogleSheetValueModel Content { get; set; }

        [JsonPropertyName("gsx$country")]
        public GoogleSheetValueModel GsxCountry { get; set; }

        [JsonPropertyName("gsx$weightclass")]
        public GoogleSheetValueModel GsxWeightClass { get; set; }

        [JsonPropertyName("gsx$name")]
        public GoogleSheetValueModel GsxName { get; set; }

        [JsonPropertyName("gsx$result")]
        public GoogleSheetValueModel GsxResult { get; set; }

        [JsonPropertyName("gsx$score")]
        public GoogleSheetValueModel GsxScore { get; set; }

        [JsonPropertyName("gsx$country_2")]
        public GoogleSheetValueModel GsxCountry2 { get; set; }

        [JsonPropertyName("gsx$name_2")]
        public GoogleSheetValueModel GsxName2 { get; set; }

        [JsonPropertyName("gsx$date")]
        public GoogleSheetValueModel GsxDate { get; set; }

        [JsonPropertyName("gsx$tournament")]
        public GoogleSheetValueModel GsxTournament { get; set; }

        [JsonPropertyName("gsx$round")]
        public GoogleSheetValueModel GsxRound { get; set; }

        [JsonPropertyName("gsx$video")]
        public GoogleSheetValueModel GsxVideo { get; set; }

    }

    public class GoogleSheetValueModel
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("$t")]
        public string Value { get; set; }

    }
}
