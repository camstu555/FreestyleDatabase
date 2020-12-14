using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreestyleDatabase.Shared.Models
{
    public class SearchCollectionResponseModel<T> where T : class, new()
    {
        public SearchCollectionResponseModel()
        {
            Items = new List<T>();
        }

        [JsonProperty(PropertyName = "value")]
        public List<T> Items { get; private set; }

        [JsonProperty(PropertyName = "@odata.count")]
        public int Total { get; set; }
    }
}