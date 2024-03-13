using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryManager.Shared.Models.DTOs
{
    public record WordMeaningDto(string LexemaType, string[] Senses);
}
