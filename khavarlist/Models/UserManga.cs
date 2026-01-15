using khavarlist.Areas.Identity.Data;

namespace khavarlist.Models
{
    public class UserManga
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public required int MangaId { get; set; }

        public required string ReadStatus { get; set; }
        public int? Progress { get; set; }
        public int? Score { get; set; }
        public int? Duration { get; set; }
        public User? User { get; set; }
        public Manga? Manga { get; set; }
    }
}
