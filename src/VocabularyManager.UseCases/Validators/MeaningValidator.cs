using FluentValidation;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.UseCases.Validators
{
    public class MeaningValidator : AbstractValidator<Meaning>
    {
        public MeaningValidator()
        {
            RuleFor(meaning => meaning.LexemeType)
                .MaximumLength(100)
                .WithMessage("Lexeme type must not exceed 100 characters.");

            RuleFor(meaning => meaning.Definition)
                .MaximumLength(2000)
                .WithMessage("Definition must not exceed 2000 characters.");

            RuleFor(meaning => meaning.Level)
                .MaximumLength(50)
                .WithMessage("Level must not exceed 50 characters.");
        }
    }
}
