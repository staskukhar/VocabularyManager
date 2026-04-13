using VocabularyManager.BlazorApp.Models;

namespace VocabularyManager.BlazorApp.Interfaces
{
    public interface IRecentlyOpenedService
    {
        Task RecordWord(int id, string name);
        Task RecordVocabulary(int id, string name);
        Task<List<RecentItem>> GetRecentWords();
        Task<List<RecentItem>> GetRecentVocabularies();
    }
}
