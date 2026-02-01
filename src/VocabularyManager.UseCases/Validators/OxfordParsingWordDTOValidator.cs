using FluentValidation;
using VocabularyManager.UseCases.DTOs;

namespace VocabularyManager.UseCases.Validators
{
    public class OxfordParsingWordDTOValidator : AbstractValidator<WordDTO>
    {
        public OxfordParsingWordDTOValidator()
        {
            RuleFor(w => w.WordContent)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull();

            RuleFor(w => w.Meanings)
                .NotEmpty()
                .WithMessage("Word must have at least one meaning.");

            RuleForEach(w => w.Meanings).ChildRules(meaning =>
            {
                meaning.RuleFor(m => m.LexemeType)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .NotNull();

                meaning.RuleFor(m => m.Level)
                    .Cascade(CascadeMode.Stop)
                    .NotEmpty()
                    .NotNull();
            });
        }
    }
}
