namespace VocabularyManager.BlazorApp.Interfaces
{
    public interface IStateManager
    {
        public void SubscribeOnChangeAction(Action action);
        public void UnsubscribeOnChangeAction(Action action);
        public void NotifyStateHasChanged();
    }
}
