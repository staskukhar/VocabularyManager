using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryParserLib.Services
{
    public interface IImageParser<T>
    {
        public Task<T> GetImageLinksByWordAsync(string url, string word, bool colorfulFilter = true);
    }
}
