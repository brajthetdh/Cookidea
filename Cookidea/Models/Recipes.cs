namespace QuickType
{
    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class Query
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("recipes")]
        public Recipe[] Recipes { get; set; }
    }

    public partial class Recipe
    {
        [JsonProperty("publisher")]
        public string Publisher { get; set; }

        [JsonProperty("f2f_url")]
        public string F2FUrl { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("source_url")]
        public string SourceUrl { get; set; }

        [JsonProperty("recipe_id")]
        public string RecipeId { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("social_rank")]
        public double SocialRank { get; set; }

        [JsonProperty("publisher_url")]
        public string PublisherUrl { get; set; }
    }

    public partial class Query
    {
        public static Query FromJson(string json) => JsonConvert.DeserializeObject<Query>(json, QuickType.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Query self) => JsonConvert.SerializeObject(self, QuickType.Converter.Settings);
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
