using GameDownloader.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameDownloader.Repository.Mapping
{
    public class GameMapping : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("GAME");
            builder.HasKey(p => p.idGame);
            builder.Property(p => p.idGame).ValueGeneratedOnAdd();
            builder.Property(p => p.GameName).IsRequired();
            builder.Property(p => p.GameFile).IsRequired();
        }
    }
}
