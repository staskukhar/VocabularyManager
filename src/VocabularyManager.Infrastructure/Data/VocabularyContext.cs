using Microsoft.EntityFrameworkCore;
using VocabularyManager.Core.Entities;
using VocabularyManager.Infrastructure.Data.Configurations;

namespace VocabularyManager.Infrastructure.Data
{
    public class VocabularyContext : DbContext
    {
        public DbSet<Vocabulary> Vocabularies { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Meaning> Meanings { get; set; }

        public VocabularyContext(DbContextOptions<VocabularyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new VocabularyConfiguration());
            modelBuilder.ApplyConfiguration(new WordConfiguration());
            modelBuilder.ApplyConfiguration(new MeaningConfiguration());
        }
    }
}
