using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SemanaAcademica.Api.Configuration
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is System.Text.Json.JsonException || context.Exception is FormatException)
            {
                var response = new
                {
                    Message = "JSON inválido ou campo com formato incorreto.",
                    Detail = context.Exception.Message
                };

                context.Result = new BadRequestObjectResult(response);
                context.ExceptionHandled = true;
            }
        }
    }

    public static class MvcConfigurationExtensions
    {
        public static IServiceCollection AddMvcWithJsonExceptionFilter(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<JsonExceptionFilter>();
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(x => x.Value?.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    var payload = new
                    {
                        message = "Dados de entrada inválidos",
                        errors
                    };

                    return new BadRequestObjectResult(payload);
                };
            });

            return services;
        }
    }
}
