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
                .Where(ua => ua.UserId == userId)
                .Include(ua => ua.Anime)
                .ToListAsync();
        }

        public async Task<AnimeStats> GetUserAnimeStats(string userId)
        {
            var userAnimes = await _context.UserAnimes
                .Where(ua => ua.UserId == userId)
                .Include(ua => ua.Anime)
                .ToListAsync();

            var animeStats = new AnimeStats
            {
                // Status counts
                Watching = userAnimes.Count(ua => ua.WatchStatus == "Watching"),
                Completed = userAnimes.Count(ua => ua.WatchStatus == "Completed"),
                OnHold = userAnimes.Count(ua => ua.WatchStatus == "On_Hold"),
                Dropped = userAnimes.Count(ua => ua.WatchStatus == "Dropped"),
                PlanToWatch = userAnimes.Count(ua => ua.WatchStatus == "Plan_To_Watch"),

                // Total episodes watched
                TotalEpisodes = userAnimes.Sum(ua => ua.Progress ?? 0),

                // Average score (excluding 0/null scores)
                AverageScore = Math.Round(userAnimes.Where(ua => ua.Score > 0)
                                          .Any()
                                ? (double)userAnimes.Where(ua => ua.Score > 0)
                                                .Average(ua => ua.Score ?? 0): 0,2),

                Days = Math.Round((double)(userAnimes.Sum(ua => (ua.Progress ?? 0) * (ua.Duration ?? 0)) / 1440.0),2)
            };

            return animeStats;
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

        public async Task UpdateAnimeProgress(string userId, int animeId, int episodesWatched,string duration)
        {

            var userAnime = await _context.UserAnimes
                .Include(ua => ua.Anime)
                .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.AnimeId == animeId);

            if (userAnime != null)
            {
                userAnime.Progress = episodesWatched;

                int durationInMinutes = ParseDuration(duration);
                userAnime.Duration = durationInMinutes * episodesWatched;
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
        public int ParseDuration(string duration)
        {
            int totalMinutes = 0;
            var parts = duration.ToLower().Split(' ');

            for (int i = 0; i < parts.Length; i++)
            {
                if (int.TryParse(parts[i], out int value))
                {
                    if (i + 1 < parts.Length)
                    {
                        if (parts[i + 1].Contains("hr"))
                            totalMinutes += value * 60;
                        else if (parts[i + 1].Contains("min"))
                            totalMinutes += value;
                    }
                }
            }

            return totalMinutes;
        }

    }


}
