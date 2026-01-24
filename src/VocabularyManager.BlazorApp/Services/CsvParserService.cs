namespace VocabularyManager.BlazorApp.Services
{
    public class CsvParserService
    {
        private const char DefaultDelimiter = ',';

        /// <summary>
        /// Parses CSV content and extracts column headers from the first row.
        /// </summary>
        public CsvParseResult ParseCsv(IList<string> csvLines, char delimiter = DefaultDelimiter)
        {
            if (csvLines.Count == 0)
            {
                return new CsvParseResult
                {
                    IsSuccess = false,
                    ErrorMessage = "CSV content is empty"
                };
            }

            List<string> headers = ParseLine(csvLines[0], delimiter);
            if (headers.Count == 0)
            {
                return new CsvParseResult
                {
                    IsSuccess = false,
                    ErrorMessage = "No columns found in CSV header"
                };
            }

            List<List<string>> dataRows = new List<List<string>>();
            for (int i = 1; i < csvLines.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(csvLines[i]))
                {
                    dataRows.Add(ParseLine(csvLines[i], delimiter));
                }
            }

            return new CsvParseResult
            {
                IsSuccess = true,
                Headers = headers,
                DataRows = dataRows
            };
        }

        /// <summary>
        /// Parses a single CSV line handling quoted values.
        /// </summary>
        private List<string> ParseLine(string line, char delimiter)
        {
            List<string> result = new List<string>();
            System.Text.StringBuilder currentField = new System.Text.StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        // Escaped quote
                        currentField.Append('"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == delimiter && !inQuotes)
                {
                    result.Add(currentField.ToString().Trim());
                    currentField.Clear();
                }
                else
                {
                    currentField.Append(c);
                }
            }

            result.Add(currentField.ToString().Trim());
            return result;
        }
    }

    public class CsvParseResult
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
        public List<string> Headers { get; set; } = new();
        public List<List<string>> DataRows { get; set; } = new();
    }
}
