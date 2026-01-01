using FluentValidation;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.UseCases.Validators
{
    public class VocabularyValidator : AbstractValidator<Vocabulary>
    {
        public VocabularyValidator()
        {
            RuleFor(wl => wl.Name)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("List's name is required.")
                .NotEmpty();
        }
    }
}
