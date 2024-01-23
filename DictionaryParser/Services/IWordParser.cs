using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryParser.Services
{
    public interface IWordParser<T>
    {
        public Task<IEnumerable<T>> GetWordListByLinkAsync(string link);
        public T GetDataAsObject(IElement element);
    }
}
