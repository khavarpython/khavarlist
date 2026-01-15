using khavarlist.Models;

namespace khavarlist.Services
{
    public interface IAnimeService
    {
        Task<Anime?> GetOrCreateAnime(int apiAnimeId);
        Task<List<UserAnime>> GetUserAnimes(string userId);
        Task<AnimeStats> GetUserAnimeStats(string userId);
        Task<UserAnime> AddAnimeToList(string userId, int apiAnimeID, string WatchStatus);
        Task UpdateAnimeStatus(string userId, int animeId, string WatchStatus);
        Task UpdateAnimeProgress(string userId, int animeId,int episodesWatched,string duration);
        Task UpdateAnimeScore(string userId, int animeId, int score);
        Task RemoveAnimeFromList(string userId, int animeId);
        Task<bool> IsAnimeInUserList(string userId, int animeId);
        Task<UserAnime> GetUserAnimeDetails(string userId, int animeId);
        int ParseDuration(string duration);
    }
}
