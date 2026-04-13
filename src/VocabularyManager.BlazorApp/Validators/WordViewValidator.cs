using FluentValidation;
using Microsoft.Extensions.Localization;
using VocabularyManager.BlazorApp.Models.Views;
using VocabularyManager.BlazorApp.Resources;

namespace VocabularyManager.BlazorApp.Validators
{
    public class WordViewValidator : AbstractValidator<WordView>
    {
        public WordViewValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(word => word.WordContent)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(localizer["Validation_WordContentRequired"])
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage(localizer["Validation_WordContentMaxLength"]);
        }
    }
}
