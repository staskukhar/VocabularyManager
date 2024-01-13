using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxfordDictionaryParser.Exceptions
{
    public class TheSourceIsNotAppropriateException : Exception
    {
        public TheSourceIsNotAppropriateException() { }
        public TheSourceIsNotAppropriateException(string message) 
            : base(message) { }
        public TheSourceIsNotAppropriateException(string message, Exception inner) 
            : base(message, inner) { }
    }
}
