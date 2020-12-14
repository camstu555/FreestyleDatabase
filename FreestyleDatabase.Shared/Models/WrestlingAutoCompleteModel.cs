using Newtonsoft.Json;

namespace FreestyleDatabase.Shared.Models
{
    public class WrestlingAutoCompleteModel
    {
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "queryPlusText")]
        public string QueryPlusText { get; set; }
    }
}