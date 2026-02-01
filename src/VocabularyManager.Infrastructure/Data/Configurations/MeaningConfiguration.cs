using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.Infrastructure.Data.Configurations
{
    public class MeaningConfiguration : IEntityTypeConfiguration<Meaning>
    {
        public void Configure(EntityTypeBuilder<Meaning> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Id)
                .HasIdentityOptions(startValue: 1);

            builder.Property(m => m.LexemeType)
                .HasMaxLength(100);

            builder.Property(m => m.Definition)
                .HasMaxLength(2000);

            builder.Property(m => m.Level)
                .HasMaxLength(50);
        }
    }
}
