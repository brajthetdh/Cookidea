using System.Globalization;
using Cookidea.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace QuickType
{
    public class Query
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("recipes")]
        public Recipe[] Recipes { get; set; }

        public static Query FromJson(string json) => JsonConvert.DeserializeObject<Query>(json, QuickType.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
