using GameDownloader.Domain;
using GameDownloader.Repository.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GameDownloader.Repository.Context
{
    public class GameDownloaderContext : DbContext
    {
        public GameDownloaderContext(DbContextOptions<GameDownloaderContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GameMapping());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Game> pokemon { get; set; }
    }
}
