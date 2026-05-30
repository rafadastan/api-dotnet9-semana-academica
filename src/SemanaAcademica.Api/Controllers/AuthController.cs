using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SemanaAcademica.Domain.Notifications;

namespace SemanaAcademica.Api.Controllers
{
    [Authorize]
    [Route("login")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        private readonly NotificationContext _notification;

        public AuthController(NotificationContext notification) 
            : base(notification)
        {
            _notification = notification;
        }
    }
}
