namespace khavarlist.Models
{
    public class Manga
    {
        public int MalId { get; set; }
        public required string Title { get; set; }
        public string? Image { get; set; }
        public int? TotalChapters { get; set; }
    }
}
