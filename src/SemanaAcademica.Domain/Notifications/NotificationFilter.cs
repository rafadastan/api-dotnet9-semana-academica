using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net;

namespace SemanaAcademica.Domain.Notifications
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private readonly NotificationContext _notificationContext;

        public NotificationFilter(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }

        /// <summary>
        /// Middware Intercepta a execução do resultado e, caso existam notificações pendentes,
        /// interrompe o pipeline e retorna uma resposta <c>400 Bad Request</c> com a
        /// lista de notificações serializada em JSON.
        /// </summary>
        /// <param name="context">
        /// Contexto da execução do resultado, contendo informações da requisição
        /// e da resposta HTTP atual.
        /// </param>
        /// <param name="next">
        /// Delegate que representa o próximo passo no pipeline de execução do resultado.
        /// Invocado apenas quando não há notificações pendentes.
        /// </param>
        /// <returns>
        /// Uma <see cref="Task"/> que representa a operação assíncrona.
        /// </returns>
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_notificationContext.HasNotifications)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = "application/json";

                var notifications = JsonConvert.SerializeObject(_notificationContext.Notifications);
                await context.HttpContext.Response.WriteAsync(notifications);

                return;
            }

            await next();
        }
    }
}
