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
            RuleFor(w => w.Lexeme)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull();
            RuleFor(w => w.LevelAttribute)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .NotNull();
        }
    }
}
