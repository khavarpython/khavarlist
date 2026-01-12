using khavarlist.Controllers;
using khavarlist.Data;
using khavarlist.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace khavarlist.Services
{
    public class MangaService : IMangaService
    {
        private readonly AuthDbContext _context;
        private readonly ApiController apiController;

        public MangaService(AuthDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            apiController = new ApiController(httpClientFactory);
        }

        public async Task<Manga?> GetOrCreateManga(int apiMangaId)
        {
            var manga = await _context.Mangas
                .FirstOrDefaultAsync(a => a.MalId == apiMangaId);

            if (manga != null)
            {
                return manga;
            }
            var newMangaData = await apiController.GetMangaById(apiMangaId);
            if (newMangaData == null) return null;

            manga = new Manga
            {
                MalId = apiMangaId,
                Title = newMangaData.Data.Title,
                Image = newMangaData.Data.Images.Jpg.ImageUrl,
                TotalChapters = newMangaData.Data.Chapters
            };
            _context.Mangas.Add(manga);
            await _context.SaveChangesAsync();
            return manga;

        }

        public async Task<UserManga> AddMangaToList(string userId, int apiMangaId, string readStatus)
        {
            var manga = await GetOrCreateManga(apiMangaId);
            if (manga == null)
            {
                throw new Exception("Manga not found in api");
            }
            var existing = await _context.UserMangas
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MangaId == manga.MalId);

            if (existing != null)
                throw new Exception("Manga already in your list");

            var userManga = new UserManga
            {
                UserId = userId,
                MangaId = manga.MalId,
                ReadStatus = readStatus,
                Progress = 0,
                Manga = manga

            };

            _context.UserMangas.Add(userManga);
            await _context.SaveChangesAsync();

            return userManga;
        }

        public async Task<List<UserManga>> GetUserMangas(string userId)
        {
            return await _context.UserMangas
                .Where(um => um.UserId == userId)
                .Include(um => um.Manga)
                .ToListAsync();
        }

        public async Task RemoveMangaFromList(string userId, int mangaId)
        {
            var userManga = await _context.UserMangas
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MangaId == mangaId);

            if (userManga != null)
            {
                _context.UserMangas.Remove(userManga);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateMangaStatus(string userId, int mangaId, string readStatus)
        {
            var userManga = await _context.UserMangas
                .Include(um => um.Manga)
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MangaId == mangaId);

            if (userManga != null)
            {
                userManga.ReadStatus = readStatus;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateMangaProgress(string userId, int mangaId, int chaptersRead)
        {
            var userManga = await _context.UserMangas
                .Include(um => um.Manga)
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MangaId == mangaId);

            if (userManga != null)
            {
                userManga.Progress = chaptersRead;

                if (userManga.Manga.TotalChapters.HasValue &&
                    chaptersRead >= userManga.Manga.TotalChapters.Value)
                {
                    userManga.ReadStatus = "Completed";
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateMangaScore(string userId, int mangaId, int score)
        {
            var userManga = await _context.UserMangas
                .Include(um => um.Manga)
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MangaId == mangaId);

            if (userManga != null)
            {
                userManga.Score = score;

                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> IsMangaInUserList(string userId, int mangaId)
        {
            return await _context.UserMangas
            .AnyAsync(um => um.UserId == userId && um.MangaId == mangaId);

        }

        public async Task<UserManga> GetUserMangaDetails(string userId, int mangaId)
        {
            var existing = await _context.UserMangas
                .Include(um => um.Manga)
                .FirstOrDefaultAsync(um => um.UserId == userId && um.MangaId == mangaId);

            return existing;

        }

    }
}
