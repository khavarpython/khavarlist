namespace khavarlist.Models
{
    public class ProfileView
    {
        public List<UserAnime>? UserAnimes { get; set; }
        public AnimeStats? AnimeStats { get; set; }
        public List<UserManga>? UserMangas { get; set; }
        public MangaStats? MangaStats { get; set; }
    }

    public class MangaStats
    {
        public double Days { get; set; }
        public double AverageScore { get; set; }
        public int TotalChapters { get; set; }

        // Status counts
        public int Reading { get; set; }
        public int Completed { get; set; }
        public int OnHold { get; set; }
        public int Dropped { get; set; }
        public int PlanToRead { get; set; }
        public int TotalEntries => Reading + Completed + OnHold + Dropped + PlanToRead;
    }

    public class AnimeStats
    {
        public double Days { get; set; }
        public double AverageScore { get; set; }
        public int TotalEpisodes { get; set; } 

        // Status counts
        public int Watching { get; set; }
        public int Completed { get; set; }
        public int OnHold { get; set; }
        public int Dropped { get; set; }
        public int PlanToWatch { get; set; }
        public int TotalEntries => Watching + Completed + OnHold + Dropped + PlanToWatch;
    }
}