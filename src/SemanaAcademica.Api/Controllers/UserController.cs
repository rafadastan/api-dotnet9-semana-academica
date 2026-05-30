using Microsoft.AspNetCore.Mvc;
using SemanaAcademica.Domain.Notifications;

namespace SemanaAcademica.Api.Controllers
{
    [Route("usuario")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        private readonly NotificationContext _notification;

        public UserController(NotificationContext notification) 
            : base(notification)
        {
            _notification = notification;
        }
    }
}
