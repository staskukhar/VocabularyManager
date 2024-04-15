using DictionaryManager.Shared.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace DictionaryManagerAPI.Services.Data
{
    public class VocabularyContext : DbContext
    {
        public DbSet<WordList> WordLists { get; set; }
        public DbSet<Word> Words { get; set; }
        public VocabularyContext(
            DbContextOptions<VocabularyContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WordList>()
                .Property(wl => wl.Id)
                .HasIdentityOptions(startValue: 1);

            modelBuilder.Entity<Word>()
                .Property(w => w.Id)
                .HasIdentityOptions(startValue: 1);

            modelBuilder.Entity<WordList>()
                .HasMany(wl => wl.Words)
                .WithOne(w => w.WordList)
                .HasForeignKey(w => w.WordListId)
                .HasPrincipalKey(w => w.Id);
        }

        internal IEnumerable<WordList> Where(Func<bool> condition)
        {
            throw new NotImplementedException();
        }
    }
}
