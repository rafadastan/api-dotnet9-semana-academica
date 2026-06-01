using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SemanaAcademica.Application.Contracts;
using SemanaAcademica.Application.Model.Customer;
using SemanaAcademica.Domain.Notifications;

namespace SemanaAcademica.Api.Controllers
{
    [Authorize]
    [Route("api/customer")]
    public class CustomerController : ApiControllerBase
    {
        private readonly ICustomerApplicationService _customerApplicationService;

        public CustomerController(
            ICustomerApplicationService customerApplicationService,
            NotificationContext notification)
            : base(notification)
        {
            _customerApplicationService = customerApplicationService;
        }

        /// <summary>
        /// Retorna todos os clientes cadastrados
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _customerApplicationService.GetAllAsync();
            return CustomResponse(result);
        }

        /// <summary>
        /// Retorna um cliente pelo Id
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _customerApplicationService.GetByIdAsync(id);
            return CustomResponse(result);
        }

        /// <summary>
        /// Cadastra um novo cliente
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CustomerModel model)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var result = await _customerApplicationService.AddAsync(model);
            return CustomResponse(result);
        }

        /// <summary>
        /// Atualiza os dados de um cliente
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(Guid id, [FromBody] CustomerModel model)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var result = await _customerApplicationService.UpdateAsync(id, model);
            return CustomResponse(result);
        }

        /// <summary>
        /// Remove um cliente pelo Id
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _customerApplicationService.DeleteAsync(id);
            return CustomResponse();
        }
    }
}