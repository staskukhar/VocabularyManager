using DictionaryManager.Shared.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryParserLib.Services
{
    public class OxfordDictionaryValidator
    {
        public static bool IsWordObjectDataValid(WordDTO word)
        {
            if (String.IsNullOrEmpty(word.WordContent)) return false;
            if (String.IsNullOrEmpty(word.Lexeme)) return false;
            if (String.IsNullOrEmpty(word.LevelAttribute)) return false;
            return true;
        }
    }
}
