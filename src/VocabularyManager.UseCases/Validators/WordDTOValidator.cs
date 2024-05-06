using FluentValidation;
using VocabularyManager.UseCases.DTOs;

namespace VocabularyManager.UseCases.Validators
{
    public class WordDTOValidator : AbstractValidator<WordDTO>
    {
        public WordDTOValidator()
        {
            RuleFor(word => word.WordContent)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Word content is required.")
                .NotEmpty();
        }
    }
}
