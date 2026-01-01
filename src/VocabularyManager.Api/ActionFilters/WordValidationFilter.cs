using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.Api.ActionFilters
{
    public class WordValidationFilter : IActionFilter
    {
        IValidator<Word> _validator;
        public WordValidationFilter(IValidator<Word> validator)
        {
            _validator = validator;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is Word);
            ValidationResult result = _validator.Validate(param.Value as Word);

            if (!result.IsValid)
            {
                context.Result = new BadRequestObjectResult(result.Errors);
            }
        }
    }
}
