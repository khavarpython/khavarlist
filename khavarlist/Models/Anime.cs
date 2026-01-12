namespace khavarlist.Models
{
    public class Anime
    {
        public int MalId { get; set; }
        public required string Title { get; set; }
        public string? Image{ get; set; }
        public int? TotalEpisodes { get; set; }

    }
}
