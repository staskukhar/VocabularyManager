using FluentValidation;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.UseCases.Validators
{
    public class WordValidator : AbstractValidator<Word>
    {
        public WordValidator()
        {
            RuleFor(word => word.WordContent)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Word content is required.")
                .NotEmpty();
        }
    }
}
