using System.Text;
using Ardalis.Specification;
using VocabularyManager.Core.Entities;
using VocabularyManager.Core.Specifications;
using VocabularyManager.UseCases.DTOs;
using VocabularyManager.UseCases.Interfaces;

namespace VocabularyManager.UseCases.Services
{
    public class AnkiExportService : IAnkiExportService
    {
        private readonly IRepositoryBase<Vocabulary> _vocabularyRepository;

        public AnkiExportService(IRepositoryBase<Vocabulary> vocabularyRepository)
        {
            _vocabularyRepository = vocabularyRepository;
        }

        public async Task<AnkiExportResult?> ExportAsync(int vocabularyId, CancellationToken ct = default)
        {
            var spec = new VocabularyWithWordsAndMeaningsSpecification(vocabularyId);
            var vocabulary = await _vocabularyRepository.FirstOrDefaultAsync(spec, ct);
            if (vocabulary is null) return null;

            var sb = new StringBuilder();
            sb.Append("#separator:tab\n");
            sb.Append("#html:true\n");
            sb.Append("#notetype:Basic (and reversed card)\n");
            sb.Append($"#deck:{EscapeHeaderValue(vocabulary.Name)}\n");

            foreach (var word in vocabulary.Words)
            {
                var meaningLines = word.Meanings
                    .Where(m => !string.IsNullOrEmpty(m.Definition))
                    .Select(RenderMeaning)
                    .ToList();

                if (meaningLines.Count == 0) continue;

                string front = EscapeHtml(word.WordContent);
                string back = string.Join("<br>", meaningLines);
                sb.Append(front).Append('\t').Append(back).Append('\n');
            }

            return new AnkiExportResult(
                Encoding.UTF8.GetBytes(sb.ToString()),
                SanitizeFileName(vocabulary.Name, vocabularyId));
        }

        private static string RenderMeaning(Meaning m)
        {
            string def = EscapeHtml(m.Definition!);
            return string.IsNullOrEmpty(m.LexemeType)
                ? def
                : $"{def} [{EscapeHtml(m.LexemeType)}]";
        }

        private static string EscapeHtml(string text) =>
            text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");

        private static string EscapeHeaderValue(string text) =>
            text.Replace("\n", " ").Replace("\r", " ").Replace("\t", " ");

        private static string SanitizeFileName(string name, int fallbackId)
        {
            var invalid = Path.GetInvalidFileNameChars();
            var sanitized = new string(name.Where(c => !invalid.Contains(c)).ToArray()).Trim();
            return string.IsNullOrEmpty(sanitized)
                ? $"vocabulary-{fallbackId}.txt"
                : $"{sanitized}.txt";
        }
    }
}
