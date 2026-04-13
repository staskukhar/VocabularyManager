using FluentValidation;
using Microsoft.Extensions.Localization;
using VocabularyManager.BlazorApp.Models.Views;
using VocabularyManager.BlazorApp.Resources;

namespace VocabularyManager.BlazorApp.Validators
{
    public class VocabularyViewValidator : AbstractValidator<VocabularyView>
    {
        public VocabularyViewValidator(IStringLocalizer<SharedResource> localizer) 
        {
            RuleFor(wl => wl.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage(localizer["Validation_ListNameRequired"])
                .NotEmpty();
        }
    }
}
