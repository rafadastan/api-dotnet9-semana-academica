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
        /*
         Pseudocódigo (passo a passo detalhado):
         1. Receber a instância de IServiceCollection chamada 'services'.
         2. Chamar services.AddControllers(...) para registrar os controllers MVC:
            2.1. No callback de opções, adicionar o filtro JsonExceptionFilter à coleção de filtros:
                 options.Filters.Add<JsonExceptionFilter>();
         3. Encadear ConfigureApiBehaviorOptions(...) para sobrescrever o comportamento padrão
            de respostas quando o ModelState estiver inválido:
            3.1. Atribuir uma fábrica para InvalidModelStateResponseFactory que:
                 3.1.1. Itera sobre context.ModelState e seleciona entradas com erros.
                 3.1.2. Constrói um dicionário de campo -> lista de mensagens de erro.
                 3.1.3. Cria um objeto payload com 'message' e 'errors'.
                 3.1.4. Retorna um BadRequestObjectResult contendo o payload.
         4. Retornar a mesma instância de IServiceCollection para permitir encadeamento de chamadas.
         5. Observações:
            - O método estende IServiceCollection como um método de extensão.
            - Não altera o comportamento de exceções que não sejam JsonException ou FormatException.
            - Uso típico: no Program.cs chamar 'services.AddMvcWithJsonExceptionFilter();'
        */

        /// <summary>
        /// Registra os controllers MVC e adiciona tratamento consistente para JSON inválido e
        /// falhas de validação de modelo.
        /// </summary>
        /// <param name="services">A coleção de serviços onde serão registrados os controllers e filtros.</param>
        /// <returns>Retorna a mesma instância de <see cref="IServiceCollection"/> para permitir chaining.</returns>
        /// <remarks>
        /// Este método:
        /// - Adiciona controllers via <c>services.AddControllers</c> e inclui o filtro
        ///   <c>JsonExceptionFilter</c>, que converte <c>System.Text.Json.JsonException</c>
        ///   e <c>FormatException</c> em respostas HTTP 400 com um payload explicativo.
        /// - Personaliza a resposta padrão para ModelState inválido através de
        ///   <c>ConfigureApiBehaviorOptions</c>, retornando um objeto JSON com a mensagem
        ///   "Dados de entrada inválidos" e um dicionário de erros por campo.
        ///
        /// Exemplo de uso (Program.cs):
        /// <code>
        /// services.AddMvcWithJsonExceptionFilter();
        /// </code>
        /// </remarks>
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
