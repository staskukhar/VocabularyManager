using FluentValidation;
using VocabularyManager.BlazorApp.Models.Views;

namespace VocabularyManager.BlazorApp.Validators
{
    public class WordViewValidator : AbstractValidator<WordView>
    {
        public WordViewValidator()
        {
            RuleFor(word => word.WordContent)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Word content is required.")
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("Word content must not exceed 200 characters.");
        }
    }
}
