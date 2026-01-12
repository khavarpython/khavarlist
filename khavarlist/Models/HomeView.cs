using khavarlist.Controllers;

namespace khavarlist.Models
{
    public class HomeView
    {
        public List<JikanAnimeData>? TopAnime { get; set; }
        public List<JikanMangaData>? TopManga { get; set; }
        public List<JikanAnimeData>? CurrentAnime { get; set; }
    }
}
