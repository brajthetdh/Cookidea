using Newtonsoft.Json;

namespace Cookidea.Models
{
    public class Recipe
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
}
