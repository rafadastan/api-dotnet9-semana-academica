using AutoMapper;
using SemanaAcademica.Application.Contracts;
using SemanaAcademica.Application.Model.Customer;
using SemanaAcademica.Domain.Contracts.Domain;
using SemanaAcademica.Domain.Entities;
using SemanaAcademica.Domain.Notifications;

namespace SemanaAcademica.Application.Services
{
    public class CustomerApplicationService : ICustomerApplicationService
    {
        private readonly ICustomerDomainService _customerDomainService;
        private readonly NotificationContext _notificationContext;
        private readonly IMapper _mapper;

        public CustomerApplicationService(
            ICustomerDomainService customerDomainService,
            NotificationContext notificationContext,
            IMapper mapper)
        {
            _customerDomainService = customerDomainService;
            _notificationContext = notificationContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna todos os clientes cadastrados
        /// </summary>
        public async Task<IEnumerable<CustomerModel>> GetAllAsync()
        {
            var customers = await _customerDomainService.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerModel>>(customers);
        }

        /// <summary>
        /// Retorna um cliente pelo Id
        /// </summary>
        public async Task<CustomerModel?> GetByIdAsync(Guid id)
        {
            var customer = await _customerDomainService.GetByIdAsync(id);
            return customer is null ? null : _mapper.Map<CustomerModel>(customer);
        }

        /// <summary>
        /// Cadastra um novo cliente
        /// </summary>
        public async Task<CustomerModel> AddAsync(CustomerModel model)
        {
            // Verifica CPF duplicado
            var cpfExists = await _customerDomainService.ExistsByCpfAsync(model.Cpf);
            if (cpfExists)
            {
                _notificationContext.AddNotification("Cpf", "Já existe um cliente com este CPF.");
                return model;
            }

            // Verifica Email duplicado
            var emailExists = await _customerDomainService.ExistsByEmailAsync(model.Email);
            if (emailExists)
            {
                _notificationContext.AddNotification("Email", "Já existe um cliente com este e-mail.");
                return model;
            }

            var entity = new CustomerEntity(
                model.FullName,
                model.Cpf,
                model.Email,
                model.Phone,
                model.BirthDate);

            await _customerDomainService.AddAsync(entity);

            return _mapper.Map<CustomerModel>(entity);
        }

        /// <summary>
        /// Atualiza os dados de um cliente
        /// </summary>
        public async Task<CustomerModel> UpdateAsync(Guid id, CustomerModel model)
        {
            var entity = await _customerDomainService.GetByIdAsync(id);

            if (entity is null)
            {
                _notificationContext.AddNotification("NotFound", "Cliente não encontrado.");
                return model;
            }

            entity.Update(model.FullName, model.Email, model.Phone);

            await _customerDomainService.UpdateAsync(entity);

            return _mapper.Map<CustomerModel>(entity);
        }

        /// <summary>
        /// Remove um cliente pelo Id
        /// </summary>
        public async Task DeleteAsync(Guid id)
        {
            await _customerDomainService.DeleteAsync(id);
        }
    }
}