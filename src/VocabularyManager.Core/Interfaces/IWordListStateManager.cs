namespace VocabularyManager.Core.Interfaces
{
    public interface IWordListStateManager<WL> : IStateManager
    {
        public WL? WordList { get; set; }
        Task UpdateWordList();
    }
}
