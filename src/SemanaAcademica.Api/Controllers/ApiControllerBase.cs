using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SemanaAcademica.Api.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        [ApiController]
        public abstract class ApiControllerBase : ControllerBase
        {
            private readonly NotificationContext _notificationHandler;

            protected ApiControllerBase(NotificationContext notificationHandler)
            {
                _notificationHandler = notificationHandler;
            }

            /// <summary>
            /// Retorna 200 OK com o resultado (ou apenas 200 se result for null)
            /// ou 400 BadRequest com a lista de notificações.
            /// </summary>
            protected ActionResult CustomResponse(object? result = null)
            {
                if (!_notificationHandler.HasNotifications)
                {
                    // Se vier um ModelState inválido, adiciona erros de model binding
                    if (!ModelState.IsValid)
                        AddModelErrors();

                    if (_notificationHandler.HasNotifications)
                        return BadRequest(_notificationHandler.Notifications);

                    return result is null
                        ? Ok()
                        : Ok(result);
                }

                return BadRequest(_notificationHandler.Notifications);
            }

            /// <summary>
            /// Quando você quer retornar um BadRequest a partir de um ModelState inválido
            /// </summary>
            protected ActionResult CustomResponse(ModelStateDictionary modelState)
            {
                AddModelErrors(modelState);
                return CustomResponse();
            }

            private void AddModelErrors()
            {
                AddModelErrors(ModelState);
            }

            private void AddModelErrors(ModelStateDictionary modelState)
            {
                var errors = modelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);

                foreach (var msg in errors)
                    _notificationHandler.AddNotification("", msg);
            }
        }
    }
}
