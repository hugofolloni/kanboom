using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace Kanboom.Utils
{
    public class VerificationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            var configuredApiKey = configuration["ApiSettings:ApiKey"];

            var apiKey = GetApiKeyFromRequest(context);

            if (apiKey != configuredApiKey)
            {
                var responseType = GetResponseType(context);
                object errorResponse = responseType != null
                    ? CreateErrorResponse(responseType, "API_KEY_IS_WRONG")
                    : new { Error = "API_KEY_IS_WRONG" };

                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }

        private string? GetApiKeyFromRequest(ActionExecutingContext context)
        {
            if (context.ActionArguments.Values.FirstOrDefault() is IApiKeyHolder keyHolder)
            {
                return keyHolder.ApiKey;
            }

            return context.HttpContext.Request.Headers["X-API-Key"].FirstOrDefault();
        }

        private Type? GetResponseType(ActionExecutingContext context)
        {
            if (context.ActionDescriptor is Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor descriptor)
            {
                var returnType = descriptor.MethodInfo.ReturnType;

                // If returnType is Task<T>, extract T
                if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>))
                {
                    returnType = returnType.GetGenericArguments()[0];
                }

                // If returnType is ActionResult<T>, extract T
                if (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(ActionResult<>))
                {
                    return returnType.GetGenericArguments()[0];
                }

                return returnType;
            }
            return null;
        }

        private object CreateErrorResponse(Type responseType, string message)
        {
            // Check if responseType has a static method "FromFailure"
            MethodInfo? fromFailureMethod = responseType.GetMethod("FromFailure", new[] { typeof(string) });

            if (fromFailureMethod != null)
            {
                return fromFailureMethod.Invoke(null, new object[] { message });
            }

            // If FromFailure doesn't exist, create a default response dynamically
            object instance = Activator.CreateInstance(responseType)!;

            // Set BaseResponse properties
            PropertyInfo? successProp = responseType.GetProperty("Success");
            successProp?.SetValue(instance, false);

            PropertyInfo? messageProp = responseType.GetProperty("Message");
            messageProp?.SetValue(instance, message);

            return instance;
        }
    }

    public interface IApiKeyHolder
    {
        string ApiKey { get; }
    }
}
