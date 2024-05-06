using FluentValidation;
using VocabularyManager.UseCases.DTOs;

namespace VocabularyManager.UseCases.Validators
{
    public class WordListDTOValidator : AbstractValidator<WordListDTO>
    {
        public WordListDTOValidator() 
        {
            RuleFor(wl => wl.ListName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("List's name is required.")
                .NotEmpty();
        }
    }
}
