using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.Infrastructure.Data.Configurations
{
    public class VocabularyConfiguration : IEntityTypeConfiguration<Vocabulary>
    {
        public void Configure(EntityTypeBuilder<Vocabulary> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .HasIdentityOptions(startValue: 1);

            builder.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(v => v.SourceUrl)
                .HasMaxLength(2048);

            builder.HasMany(v => v.Words)
                .WithOne(w => w.Vocabulary)
                .HasForeignKey(w => w.VocabularyId)
                .HasPrincipalKey(v => v.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
