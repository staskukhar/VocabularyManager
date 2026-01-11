namespace VocabularyManager.BlazorApp.Models;

public class ShortVocabulary(int id, string name)
{
    public int Id { get; init; } = id;

    public string Name { get; set; } = name;
}
