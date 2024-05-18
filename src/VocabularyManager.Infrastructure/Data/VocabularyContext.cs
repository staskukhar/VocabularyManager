using VocabularyManager.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace VocabularyManager.Infrastructure.Data
{
    public class VocabularyContext : DbContext
    {
        public DbSet<Vocabulary> Vocabularies { get; set; }
        public DbSet<Word> Words { get; set; }
        public VocabularyContext(
            DbContextOptions<VocabularyContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vocabulary>()
                .Property(wl => wl.Id)
                .HasIdentityOptions(startValue: 1);

            modelBuilder.Entity<Word>()
                .Property(w => w.Id)
                .HasIdentityOptions(startValue: 1);

            modelBuilder.Entity<Vocabulary>()
                .HasMany(wl => wl.Words)
                .WithOne(w => w.Vocabulary)
                .HasForeignKey(w => w.VocabularyId)
                .HasPrincipalKey(w => w.Id);
        }
    }
}
