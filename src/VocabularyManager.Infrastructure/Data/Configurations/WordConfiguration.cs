using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.Infrastructure.Data.Configurations
{
    public class WordConfiguration : IEntityTypeConfiguration<Word>
    {
        public void Configure(EntityTypeBuilder<Word> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Id)
                .HasIdentityOptions(startValue: 1);

            builder.Property(w => w.WordContent)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasIndex(w => new { w.WordContent, w.VocabularyId })
                .IsUnique();

            builder.HasMany(w => w.Meanings)
                .WithOne(m => m.Word)
                .HasForeignKey(m => m.WordId)
                .HasPrincipalKey(w => w.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
