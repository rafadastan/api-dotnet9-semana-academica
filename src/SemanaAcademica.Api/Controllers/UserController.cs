using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SemanaAcademica.Application.Contracts;
using SemanaAcademica.Application.Model.User;
using SemanaAcademica.Domain.Notifications;

namespace SemanaAcademica.Api.Controllers
{
    [Authorize]
    [Route("api/user")]
    public class UserController : ApiControllerBase
    {
        private readonly IUserApplicationService _userApplicationService;

        public UserController(
            IUserApplicationService userApplicationService,
            NotificationContext notification)
            : base(notification)
        {
            _userApplicationService = userApplicationService;
        }

        /// <summary>
        /// Retorna todos os usuários cadastrados
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userApplicationService.GetAllAsync();
            return CustomResponse(result);
        }

        /// <summary>
        /// Retorna um usuário pelo Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _userApplicationService.GetByIdAsync(id);
            return CustomResponse(result);
        }

        /// <summary>
        /// Cria um novo usuário
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] UserModel model)
        {
            var result = await _userApplicationService.CreateAsync(model);
            return CustomResponse(result);
        }

        /// <summary>
        /// Remove um usuário pelo Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userApplicationService.DeleteAsync(id);
            return CustomResponse(result);
        }
    }
}