namespace khavarlist.Models
{
    public class Anime
    {
        public required string Id { get; set; }
        public string? ViewStatus { get; set; }
        public int Progress { get; set; }
        public int Score { get; set; }
    }
}
