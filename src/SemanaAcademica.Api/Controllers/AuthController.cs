using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SemanaAcademica.Api.Controllers
{
    [Authorize]
    [Route("autenticacao")]
    [ApiController]
    public class AuthController : ControllerBase
    {
    }
}
