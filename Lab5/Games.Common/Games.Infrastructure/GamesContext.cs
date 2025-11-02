using Games.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Games.Infrastructure
{
    public class GamesContext : IdentityDbContext<ApplicationUser>
    {
        public GamesContext(DbContextOptions<GamesContext> options) : base(options) { }

        // ✅ Додай цей рядок
        public DbSet<Game> GamesBase { get; set; }

        public DbSet<Games.Infrastructure.Models.Game> Games { get; set; }
        public DbSet<PCGameModel> PCGames { get; set; }
        public DbSet<ConsoleGameModel> ConsoleGames { get; set; }
        public DbSet<GameDetailsModel> GameDetails { get; set; }
        public DbSet<PublisherModel> Publishers { get; set; }
        public DbSet<DeveloperModel> Developers { get; set; }
        public DbSet<GameDeveloperModel> GameDevelopers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // важливо для таблиць Identity

            // TPT: таблиці для бази та похідних
            modelBuilder.Entity<GameModel>().ToTable("Games");
            modelBuilder.Entity<PCGameModel>().ToTable("PCGames");
            modelBuilder.Entity<ConsoleGameModel>().ToTable("ConsoleGames");

            // PKs
            modelBuilder.Entity<GameModel>().HasKey(g => g.Id);
            modelBuilder.Entity<GameDetailsModel>().HasKey(d => d.Id);
            modelBuilder.Entity<PublisherModel>().HasKey(p => p.Id);
            modelBuilder.Entity<DeveloperModel>().HasKey(dv => dv.Id);
            modelBuilder.Entity<GameDeveloperModel>().HasKey(gd => new { gd.GameId, gd.DeveloperId });

            // 1:1 Game <-> GameDetails
            modelBuilder.Entity<GameModel>()
                .HasOne(g => g.Details)
                .WithOne(d => d.Game)
                .HasForeignKey<GameDetailsModel>(d => d.Id);

            // 1:N Publisher -> Games
            modelBuilder.Entity<PublisherModel>()
                .HasMany(p => p.Games)
                .WithOne(g => g.Publisher)
                .HasForeignKey(g => g.PublisherId)
                .OnDelete(DeleteBehavior.Cascade);

            // many-to-many via join entity
            modelBuilder.Entity<GameDeveloperModel>()
                .HasOne(gd => gd.Game)
                .WithMany(g => g.GameDevelopers)
                .HasForeignKey(gd => gd.GameId);

            modelBuilder.Entity<GameDeveloperModel>()
                .HasOne(gd => gd.Developer)
                .WithMany(dv => dv.GameDevelopers)
                .HasForeignKey(gd => gd.DeveloperId);

            // field constraints
            modelBuilder.Entity<GameModel>().Property(g => g.Name).HasMaxLength(200).IsRequired();
            modelBuilder.Entity<PublisherModel>().Property(p => p.PublisherName).HasMaxLength(150).IsRequired();
        }
    }
}
