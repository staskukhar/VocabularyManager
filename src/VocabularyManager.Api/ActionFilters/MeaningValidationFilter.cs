using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.Api.ActionFilters
{
    public class MeaningValidationFilter : IActionFilter
    {
        private readonly IValidator<Meaning> _validator;

        public MeaningValidationFilter(IValidator<Meaning> validator)
        {
            _validator = validator;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.SingleOrDefault(p => p.Value is Meaning);
            if (param.Value is Meaning meaning)
            {
                ValidationResult result = _validator.Validate(meaning);
                if (!result.IsValid)
                {
                    context.Result = new BadRequestObjectResult(result.Errors);
                }
            }
        }
    }
}
