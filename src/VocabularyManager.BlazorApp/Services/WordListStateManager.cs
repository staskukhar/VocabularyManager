using System.Net.Http.Json;
using VocabularyManager.Core.Entities;
using VocabularyManager.Core.Interfaces;
using VocabularyManager.UseCases.DTOs;

namespace VocabularyManager.BlazorApp.Services
{
    public class WordListStateManager : IWordListStateManager<WordListDTO>
    {
        private event Action? OnChange;
        private ILogger _logger;
        private WordListDTO? _wordListDTO;
        private HttpService _httpService;
        private HttpPathBuilder _httpPathBuilder;

        WordListDTO? IWordListStateManager<WordListDTO>.WordList
        {
            get => _wordListDTO;
            set => _wordListDTO = value;
        }

        public WordListStateManager(
            ILogger<IWordListStateManager<WordList>> logger,
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
                _logger.LogInformation($"{nameof(WordListStateManager)}: on change was invoked.");
            }
            else
            {
                _logger.LogInformation($"{nameof(WordListStateManager)}: no subscribers defined.");
            }
        }

        public async Task UpdateWordList()
        {
            if (_wordListDTO is null)
            {
                _logger.LogInformation($"{UpdateWordList}: word list was not initialized.");
            }
            else
            {
                HttpResponseMessage response = await _httpService.Get(
                    _httpPathBuilder.GetWordListByIdEndpoint(_wordListDTO.Id ?? -1));

                if(response.IsSuccessStatusCode)
                {
                    _wordListDTO = await response.Content.ReadFromJsonAsync<WordListDTO>();
                    NotifyStateHasChanged();
                }
                else
                {
                    _logger.LogInformation($"{UpdateWordList}: http operation failed.");
                }
            }

        }
    }
}
