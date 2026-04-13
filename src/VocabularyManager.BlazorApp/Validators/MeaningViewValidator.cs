using FluentValidation;
using Microsoft.Extensions.Localization;
using VocabularyManager.BlazorApp.Models.Views;
using VocabularyManager.BlazorApp.Resources;

namespace VocabularyManager.BlazorApp.Validators
{
    public class MeaningViewValidator : AbstractValidator<MeaningView>
    {
        public MeaningViewValidator(IStringLocalizer<SharedResource> localizer)
        {
            RuleFor(meaning => meaning.LexemeType)
                .MaximumLength(100)
                .WithMessage(localizer["Validation_LexemeTypeMaxLength"]);

            RuleFor(meaning => meaning.Definition)
                .MaximumLength(2000)
                .WithMessage(localizer["Validation_DefinitionMaxLength"]);

            RuleFor(meaning => meaning.Level)
                .MaximumLength(50)
                .WithMessage(localizer["Validation_LevelMaxLength"]);
        }
    }
}
