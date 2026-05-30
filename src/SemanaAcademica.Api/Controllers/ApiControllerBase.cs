using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SemanaAcademica.Domain.Notifications;

namespace SemanaAcademica.Api.Controllers
{
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        private readonly NotificationContext _notificationHandler;

        protected ApiControllerBase(NotificationContext notificationHandler)
        {
            _notificationHandler = notificationHandler;
        }

        protected ActionResult CustomResponse(object? result = null)
        {
            if (!_notificationHandler.HasNotifications)
            {
                if (!ModelState.IsValid)
                    AddModelErrors();

                if (_notificationHandler.HasNotifications)
                    return BadRequest(_notificationHandler.Notifications);

                return result is null ? Ok() : Ok(result);
            }

            return BadRequest(_notificationHandler.Notifications);
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            AddModelErrors(modelState);
            return CustomResponse();
        }

        private void AddModelErrors() => AddModelErrors(ModelState);

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