using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using Application.Common;

namespace API.Extensions
{
    public class FluentValidatorFilter : IActionFilter
    {
        private readonly IServiceProvider _serviceProvider;
        public FluentValidatorFilter(
                IServiceProvider serviceProvider
            )
        {
            _serviceProvider = serviceProvider;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument is null) continue;

                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());

                var validator = _serviceProvider.GetService(validatorType) as IValidator;

                if(validator is null) continue;

                var validationcontext = new ValidationContext<object>(argument);
                var result = validator.Validate(validationcontext);

                if(!result.IsValid)
                {
                    var errors = result.Errors
                                        .GroupBy(e => e.PropertyName)
                                        .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                    var errorResult = Result<object>.ValidationFailure(errors);

                    context.Result = errorResult.ToActionResult();
                    return;
                }
            }
        }
    }
}
