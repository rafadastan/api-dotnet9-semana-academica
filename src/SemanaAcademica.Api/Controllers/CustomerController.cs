using Microsoft.AspNetCore.Mvc;
using SemanaAcademica.Domain.Notifications;

namespace SemanaAcademica.Api.Controllers
{
    [Route("clientes")]
    [ApiController]
    public class CustomerController : ApiControllerBase
    {
        private readonly NotificationContext _notification;

        public CustomerController(NotificationContext notification) 
            : base(notification)
        {
            _notification = notification;
        }
    }
}
