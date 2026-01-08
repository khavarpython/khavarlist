
using khavarlist.Areas.Identity.Data;

namespace khavarlist.Models
{
    public class UserAnime
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public required int AnimeId { get; set; }

        public required string WatchStatus { get; set; }
        public int? Progress { get; set; }
        public int? Score { get; set; }

        public User? User { get; set; } 
        public Anime? Anime { get; set; }
    }
}
