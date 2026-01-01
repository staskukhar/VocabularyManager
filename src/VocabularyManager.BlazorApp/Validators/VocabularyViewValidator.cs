using FluentValidation;
using VocabularyManager.BlazorApp.Models.Views;

namespace VocabularyManager.BlazorApp.Validators
{
    public class VocabularyViewValidator : AbstractValidator<VocabularyView>
    {
        public VocabularyViewValidator() 
        {
            RuleFor(wl => wl.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("List's name is required.")
                .NotEmpty();
        }
    }
}
