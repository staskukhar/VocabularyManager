using VocabularyManager.BlazorApp.Models.Views;

namespace VocabularyManager.BlazorApp.Services
{
    public static class WordCloner
    {
        public static WordView CloneWord(WordView word)
        {
            List<MeaningView> clonedMeanings = word.Meanings.Select(m => new MeaningView
            {
                Id = m.Id,
                LexemeType = m.LexemeType,
                Definition = m.Definition,
                Level = m.Level,
                WordId = m.WordId
            }).ToList();

            return new WordView
            {
                Id = word.Id,
                WordContent = word.WordContent,
                VocabularyId = word.VocabularyId,
                Meanings = clonedMeanings
            };
        }

        public static MeaningView CloneMeaning(MeaningView meaning)
        {
            return new MeaningView
            {
                Id = meaning.Id,
                LexemeType = meaning.LexemeType,
                Definition = meaning.Definition,
                Level = meaning.Level,
                WordId = meaning.WordId
            };
        }
    }
}
