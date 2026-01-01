namespace VocabularyManager.UseCases.Exceptions
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
