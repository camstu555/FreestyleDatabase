using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreestyleDatabase.Shared.Models
{
    public class WrestlingAutoCompleteModel
    {
        [JsonProperty(PropertyName = "Text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "queryPlusText")]
        public string QueryPlusText { get; set; }
    }
}