using khavarlist.Areas.Identity.Data;
using khavarlist.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace khavarlist.Data;

public class AuthDbContext : IdentityDbContext<User>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }
    public DbSet<Anime> Animes { get; set; }
    public DbSet<UserAnime> UserAnimes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Anime>()
            .HasKey(a => a.MalId);

        builder.Entity<Anime>()
              .Property(a => a.MalId)
              .ValueGeneratedNever(); 

        builder.Entity<UserAnime>()
            .HasOne(ua => ua.User)
            .WithMany()
            .HasForeignKey(ua => ua.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserAnime>()
            .HasOne(ua => ua.Anime)
            .WithMany()
            .HasForeignKey(ua => ua.AnimeId)
            .HasPrincipalKey(a => a.MalId); 

        builder.Entity<UserAnime>()
            .HasIndex(ua => new { ua.UserId, ua.AnimeId })
            .IsUnique();
    }
}
