namespace VocabularyManager.Core.Interfaces
{
    public interface IStateManager
    {
        public void SubscribeOnChangeAction(Action action);
        public void UnsubscribeOnChangeAction(Action action);
        public void NotifyStateHasChanged();
    }
}
