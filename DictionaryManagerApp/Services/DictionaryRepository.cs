using DictionaryManager.Shared.Models.DTOs;

namespace DictionaryManagerApp.Services
{
    public class DictionaryRepository : IDictionaryRepository<WordListDTO, WordDTO>
    {
        public WordListDTO Dictionary { get; private set; }
        public bool HasChanges { get; private set; }

        private event Action OnChange;

        public DictionaryRepository() { }

        public void SetDictionary(WordListDTO dictionary)
        {
            Dictionary = dictionary;
            HasChanges = false;
        }
        public void UpdateWordAtIndex(WordDTO word, int index)
        {
            if(index < 0 || index > Dictionary?.Words?.Count - 1)
            {
                throw new IndexOutOfRangeException("The index is out of the dictionary.");
            }
            Dictionary.Words[index] = word;
            HasChanges = true;
            NotifyStateHasChanged();
        }
        public void RemoveWordAtIndex(int index)
        {
            if (index < 0 || index > Dictionary?.Words?.Count - 1)
            {
                throw new IndexOutOfRangeException("The index is out of the dictionary.");
            }
            Dictionary.Words.RemoveAt(index);
            NotifyStateHasChanged();
        }
        public void AddWord(WordDTO word) 
        {
            Dictionary.Words.Add(word);
            NotifyStateHasChanged();
        }
        public void SubscribeOnChangeAction(Action action)
        {
            OnChange += action;
        }
        public void UnsubscribeOnChangeAction(Action action) 
        {
            OnChange -= action;
        }
        public void NotifyStateHasChanged()
        {
            OnChange.Invoke();
        }
        public void ChangesApplied()
        {
            HasChanges = false;
            NotifyStateHasChanged();
        }
    }
}
