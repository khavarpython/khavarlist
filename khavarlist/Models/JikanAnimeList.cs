using khavarlist.Controllers;
using Newtonsoft.Json;

namespace khavarlist.Models
{
    public class JikanAnimeList
    {
        [JsonProperty("data")]
        public required List<JikanAnimeData> Data { get; set; }

        [JsonProperty("pagination")]
        public required Pagination Pagination { get; set; }
    }

    public class JikanAnimeSingle
    {
        [JsonProperty("data")]
        public required JikanAnimeData Data { get; set; }
    }

    public class JikanAnimeData
    {
        [JsonProperty("mal_id")]
        public int MalId { get; set; }

        [JsonProperty("title")]
        public required string Title { get; set; }

        [JsonProperty("type")]
        public required string Type { get; set; }

        [JsonProperty("source")]
        public required string Source { get; set; }

        [JsonProperty("episodes")]
        public int? Episodes { get; set; }

        [JsonProperty("synopsis")]
        public string? Synopsis { get; set; }

        [JsonProperty("background")]
        public string? Background { get; set; }

        [JsonProperty("season")]
        public string? Season { get; set; }

        [JsonProperty("year")]
        public string? Year { get; set; }

        [JsonProperty("score")]
        public double? Score { get; set; }

        [JsonProperty("rank")]
        public int? Rank { get; set; }

        [JsonProperty("trailer")]
        public Trailer? Trailer { get; set; }

        [JsonProperty("genres")]
        public List<Genre>? Genres { get; set; }

        [JsonProperty("images")]
        public required Images Images { get; set; }

        [JsonProperty("aired")]
        public Aired? Aired { get; set; }
    }

    public class Trailer
    {
        [JsonProperty("embed_url")]
        public required string? embed_url { get; set; }
    }

    public class Images
    {
        [JsonProperty("jpg")]
        public required ImageUrls Jpg { get; set; }
    }

    public class ImageUrls
    {
        [JsonProperty("image_url")]
        public required string ImageUrl { get; set; }

        [JsonProperty("large_image_url")]
        public string? LargeImageUrl { get; set; }
    }

    public class Aired
    {
        [JsonProperty("from")]
        public DateTime? From { get; set; }

        [JsonProperty("to")]
        public DateTime? To { get; set; }
    }

    public class Genre
    {
        [JsonProperty("mal_id")]
        public int? MalId { get; set; }

        [JsonProperty("type")]
        public string? Type { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }
    }

    public class Pagination
    {
        [JsonProperty("last_visible_page")]
        public int LastVisiblePage { get; set; }

        [JsonProperty("has_next_page")]
        public bool HasNextPage { get; set; }

        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }
    }
}
