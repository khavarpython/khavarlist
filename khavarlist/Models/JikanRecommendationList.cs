using Newtonsoft.Json;

namespace khavarlist.Models
{
    public class JikanRecommendationList
    {
        [JsonProperty("data")]
        public required List<JikanRecommendationData> Data { get; set; }

        [JsonProperty("pagination")]
        public required Pagination Pagination { get; set; }

    }

    public class JikanRecommendationData
    {
        [JsonProperty("mal_id")]
        public string MalId { get; set; }

        [JsonProperty("entry")]
        public List<Entry>? Entry { get; set; }

        [JsonProperty("content")]
        public string? Content { get; set; }

        [JsonProperty("user")]
        public MalUser? User { get; set; }

    }
    public class Entry
    {
        [JsonProperty("mal_id")]
        public int MalId { get; set; }

        [JsonProperty("title")]
        public required string Title { get; set; }

        [JsonProperty("images")]
        public required Images Images { get; set; }
    }

    public class MalUser {
        [JsonProperty("username")]
        public required string UserName { get; set; }
    }
}
