using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.Api.ActionFilters
{
    public class VocabularyValidationFilter : IActionFilter
    {
        IValidator<Vocabulary> _validator;
        public VocabularyValidationFilter(IValidator<Vocabulary> validator)
        {
            _validator = validator;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is Vocabulary);
            ValidationResult result = _validator.Validate(param.Value as Vocabulary);

            if (!result.IsValid)
            {
                context.Result = new BadRequestObjectResult(result.Errors);
            }
        }
    }
}
