using khavarlist.Controllers;

namespace khavarlist.Models
{
    public class TopMediaView
    {
        public List<JikanAnimeData>? TopAnime { get; set; }
        public List<JikanMangaData>? TopManga { get; set; }
    }
}
