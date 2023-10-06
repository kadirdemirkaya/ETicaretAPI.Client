using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ETicaretAPI.Client.Filters.Validation
{
    public class ErrorHandlingFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                var errorResponse = context.Exception.Message;

                if (context.Exception is FluentValidation.ValidationException validationException)
                {
                    var errorDetails = new List<ValidationError>();

                    foreach (var error in validationException.Errors)
                    {
                        var errorDetail = new ValidationError
                        {
                            Key = error.PropertyName,
                            Value = error.ErrorMessage
                        };

                        errorDetails.Add(errorDetail);
                    }

                    errorResponse = JsonConvert.SerializeObject(errorDetails);
                }

                context.HttpContext.Items["ErrorMessage"] = errorResponse;

                var result = new BadRequestObjectResult(errorResponse);
                context.Result = result;
                context.ExceptionHandled = true;
            }
        }
    }
    public class ValidationError
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}

