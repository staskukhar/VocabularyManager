namespace VocabularyManager.BlazorApp.Interfaces
{
    public interface IVocabularyStateManager<V> : IStateManager
    {
        public V? Vocabulary { get; set; }
        Task Update();
    }
}
