using khavarlist.Controllers;
using khavarlist.Data;
using khavarlist.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace khavarlist.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly AuthDbContext _context;
        private readonly ApiController apiController;

        public AnimeService(AuthDbContext context, IHttpClientFactory httpClientFactory)
        {
                _context = context;
                apiController = new ApiController(httpClientFactory);
        }

        public async Task<Anime?> GetOrCreateAnime(int apiAnimeId)
        {
            var anime = await _context.Animes
                .FirstOrDefaultAsync(a => a.MalId == apiAnimeId);

            if (anime != null)
            {
                return anime;
            }
            var newAnimeData = await apiController.GetAnimeById(apiAnimeId);
            if (newAnimeData == null) return null;

            anime = new Anime
            {
                MalId = apiAnimeId,
                Title = newAnimeData.Data.Title,
                Image = newAnimeData.Data.Images.Jpg.ImageUrl,
                TotalEpisodes = newAnimeData.Data.Episodes
            };
            _context.Animes.Add(anime);
            await _context.SaveChangesAsync();
            return anime;

        }

        public async Task<UserAnime> AddAnimeToList(string userId, int apiAnimeID, string WatchStatus)
        {
            var anime = await GetOrCreateAnime(apiAnimeID);
            if (anime == null)
            {
                throw new Exception("Anime not found in api");
            }
            var existing= await _context.UserAnimes
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.AnimeId == anime.MalId);

            if (existing != null)
                throw new Exception("Anime already in your list");

            var userAnime = new UserAnime
            {
                UserId = userId,
                AnimeId = anime.MalId,
                WatchStatus = WatchStatus,
                Progress = 0,
                Anime = anime

            };

            _context.UserAnimes.Add(userAnime);
            await _context.SaveChangesAsync();

            return userAnime;
        }


        public async Task<List<UserAnime>> GetUserAnimes(string userId)
        {
            return await _context.UserAnimes
            .Include(ua => ua.Anime)
            .Where(ua => ua.UserId == userId)
            .ToListAsync();
        }

        public async Task RemoveAnimeFromList(string userId, int animeId)
        {
            var userAnime = await _context.UserAnimes
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.AnimeId == animeId);

            if (userAnime != null)
            {
                _context.UserAnimes.Remove(userAnime);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAnimeStatus(string userId, int animeId, string WatchStatus)
        {
            var userAnime = await _context.UserAnimes
                .Include(ua => ua.Anime)
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.AnimeId == animeId);

            if (userAnime != null)
            {
                userAnime.WatchStatus = WatchStatus;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAnimeProgress(string userId, int animeId, int episodesWatched)
        {
            var userAnime = await _context.UserAnimes
                .Include(ua => ua.Anime)
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.AnimeId == animeId);

            if (userAnime != null)
            {
                userAnime.Progress = episodesWatched;

                if (userAnime.Anime.TotalEpisodes.HasValue &&
                    episodesWatched >= userAnime.Anime.TotalEpisodes.Value)
                {
                    userAnime.WatchStatus = "Completed";
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAnimeScore(string userId, int animeId, int score)
        {
            var userAnime = await _context.UserAnimes
                .Include(ua => ua.Anime)
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.AnimeId == animeId);

            if (userAnime != null)
            {
                userAnime.Score = score;

                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> IsAnimeInUserList(string userId, int animeId)
        {
            return await _context.UserAnimes
            .AnyAsync(ua => ua.UserId == userId && ua.AnimeId == animeId);
        
        }

        public async Task<UserAnime> GetUserAnimeDetails(string userId, int animeId)
        {
            var existing = await _context.UserAnimes
                .Include(ua => ua.Anime) 
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.AnimeId == animeId);

            return existing;
        
        }

    }
}
