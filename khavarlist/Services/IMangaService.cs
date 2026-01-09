using khavarlist.Models;

namespace khavarlist.Services
{
    public interface IMangaService
    {
        Task<Manga?> GetOrCreateManga(int apiMangaId);
        Task<List<UserManga>> GetUserMangas(string userId);
        Task<UserManga> AddMangaToList(string userId, int apiMangaId, string readStatus);
        Task UpdateMangaStatus(string userId, int mangaId, string readStatus);
        Task UpdateMangaProgress(string userId, int mangaId, int chaptersRead);
        Task UpdateMangaScore(string userId, int mangaId, int score);
        Task RemoveMangaFromList(string userId, int mangaId);
        Task<bool> IsMangaInUserList(string userId, int mangaId);
        Task<UserManga> GetUserMangaDetails(string userId, int mangaId);
    }
}
