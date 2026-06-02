using Microsoft.AspNetCore.Mvc;
using SemanaAcademica.Application.Contracts;
using SemanaAcademica.Application.Model.Auth;
using SemanaAcademica.Domain.Notifications;

namespace SemanaAcademica.Api.Controllers
{
    [Route("api/auth")]
    public class AuthController : ApiControllerBase
    {
        private readonly IUserApplicationService _userApplicationService;

        public AuthController(
            IUserApplicationService userApplicationService,
            NotificationContext notification) : base(notification)
        {
            _userApplicationService = userApplicationService;
        }

        /// <summary>
        /// Autentica o usuário e retorna o token de acesso
        /// </summary>
        /// <param name="model">Credenciais do usuário</param>
        /// <returns>Token de acesso</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AccessModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] AccessModel model)
        {
            var result = await _userApplicationService.GetAccessAsync(model);

            if (result is null)
                return CustomResponse();

            return CustomResponse(new { Token = result });
        }
    }
}