using System.Text.Json;
using Microsoft.JSInterop;
using VocabularyManager.BlazorApp.Interfaces;
using VocabularyManager.BlazorApp.Models;

namespace VocabularyManager.BlazorApp.Services
{
    public class RecentlyOpenedService : IRecentlyOpenedService
    {
        private const string RecentWordsKey = "recentWords";
        private const string RecentVocabulariesKey = "recentVocabularies";
        private const int MaxItems = 5;

        private readonly IJSRuntime _jsRuntime;

        public RecentlyOpenedService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task RecordWord(int id, string name, bool isGlobalMode = false)
        {
            await RecordItem(RecentWordsKey, id, name, isGlobalMode);
        }

        public async Task RecordVocabulary(int id, string name)
        {
            await RecordItem(RecentVocabulariesKey, id, name);
        }

        public async Task<List<RecentItem>> GetRecentWords()
        {
            return await GetItems(RecentWordsKey);
        }

        public async Task<List<RecentItem>> GetRecentVocabularies()
        {
            return await GetItems(RecentVocabulariesKey);
        }

        private async Task RecordItem(string key, int id, string name, bool isGlobalMode = false)
        {
            List<RecentItem> items = await GetItems(key);

            items.RemoveAll(i => i.Id == id && i.IsGlobalMode == isGlobalMode);

            items.Insert(0, new RecentItem
            {
                Id = id,
                Name = name,
                IsGlobalMode = isGlobalMode
            });

            if (items.Count > MaxItems)
                items = items.Take(MaxItems).ToList();

            string json = JsonSerializer.Serialize(items);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
        }

        private async Task<List<RecentItem>> GetItems(string key)
        {
            string? json = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", key);

            if (string.IsNullOrEmpty(json))
                return new List<RecentItem>();

            return JsonSerializer.Deserialize<List<RecentItem>>(json) ?? new List<RecentItem>();
        }
    }
}
