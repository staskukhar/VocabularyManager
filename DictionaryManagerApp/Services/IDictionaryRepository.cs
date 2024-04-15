using DictionaryManager.Shared.Models.DTOs;

namespace DictionaryManagerApp.Services
{
    public interface IDictionaryRepository<WL, W>
    {
        WL Dictionary { get; }
        bool HasChanges { get; }
        void SetDictionary(WL dictionary);
        void RemoveWordAtIndex(int index);
        void UpdateWordAtIndex(W word, int index);
        void AddWord(W word);
        void SubscribeOnChangeAction(Action action);
        void UnsubscribeOnChangeAction(Action action);
        void ChangesApplied();
        public void NotifyStateHasChanged();
    }
}
