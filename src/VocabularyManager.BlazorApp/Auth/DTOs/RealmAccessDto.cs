using System.Text.Json.Serialization;

namespace VocabularyManager.BlazorApp.Auth.DTOs;

public record RealmAccessDto
{
    [JsonPropertyName("roles")]
    public string[] Roles { get; init; } = [];
}