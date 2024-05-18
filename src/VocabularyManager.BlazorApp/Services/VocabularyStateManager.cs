using System.Net.Http.Json;
using VocabularyManager.BlazorApp.Interfaces;
using VocabularyManager.BlazorApp.Models.Views;

namespace VocabularyManager.BlazorApp.Services
{
    public class VocabularyStateManager : IVocabularyStateManager<VocabularyView>
    {
        private event Action? OnChange;
        private ILogger _logger;
        private VocabularyView? _vocabulary;
        private HttpService _httpService;
        private HttpPathBuilder _httpPathBuilder;

        VocabularyView? IVocabularyStateManager<VocabularyView>.Vocabulary
        {
            get => _vocabulary;
            set => _vocabulary = value;
        }

        public VocabularyStateManager(
            ILogger<IVocabularyStateManager<VocabularyView>> logger,
            HttpService httpService,
            HttpPathBuilder httpPathBuilder)
        {
            _logger = logger;
            _httpService = httpService;
            _httpPathBuilder = httpPathBuilder;
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
            if(OnChange != null)
            {
                OnChange.Invoke();
                _logger.LogInformation($"{nameof(VocabularyStateManager)}: on change was invoked.");
            }
            else
            {
                _logger.LogInformation($"{nameof(VocabularyStateManager)}: no subscribers defined.");
            }
        }

        public async Task Update()
        {
            if (_vocabulary is null)
            {
                _logger.LogInformation($"{Update}: word list was not initialized.");
            }
            else
            {
                HttpResponseMessage response = await _httpService.Get(
                    _httpPathBuilder.GetVocabularytByIdEndpoint(_vocabulary.Id));

                if(response.IsSuccessStatusCode)
                {
                    _vocabulary = await response.Content.ReadFromJsonAsync<VocabularyView>();
                    NotifyStateHasChanged();
                }
                else
                {
                    _logger.LogInformation($"{Update}: http operation failed.");
                }
            }

        }
    }
}
