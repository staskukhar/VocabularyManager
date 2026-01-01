using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VocabularyManager.Core.Entities;

namespace VocabularyManager.Api.ActionFilters
{
    public class WordsValidationFilter : IActionFilter
    {
        IValidator<Word> _validator;
        public WordsValidationFilter(IValidator<Word> validator)
        {
            _validator = validator;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var param = context.ActionArguments.FirstOrDefault(p => p.Value is IEnumerable<Word>);
            foreach (Word word in param.Value as IEnumerable<Word>) 
            {
                ValidationResult result = _validator.Validate(word);
                if(!result.IsValid)
                {
                    context.Result = new BadRequestObjectResult(result);
                }
            }
        }
    }
}
