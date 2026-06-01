using FluentValidation;
using SemanaAcademica.Domain.Contracts.Domain;
using SemanaAcademica.Domain.Contracts.Infra;
using SemanaAcademica.Domain.Entities;
using SemanaAcademica.Domain.Notifications;

namespace SemanaAcademica.Domain.Services
{
    public class CustomerDomainService : BaseDomainService<CustomerEntity>, ICustomerDomainService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerDomainService(
            IBaseRepository<CustomerEntity> baseRepository,
            NotificationContext notificationContext,
            IValidator<CustomerEntity> customerValidator,
            ICustomerRepository customerRepository)
            : base(baseRepository, notificationContext, customerValidator)
        {
            _customerRepository = customerRepository;
        }

        public override Task<CustomerEntity?> GetByIdAsync(Guid id)
        {
            return _customerRepository.GetByIdAsync(id);
        }

        public override Task<IEnumerable<CustomerEntity>> GetAllAsync()
        {
            return _customerRepository.GetAllAsync();
        }

        public Task<bool> ExistsByCpfAsync(string cpf) =>
            _customerRepository.ExistsByCpfAsync(cpf);

        public Task<bool> ExistsByEmailAsync(string email) =>
            _customerRepository.ExistsByEmailAsync(email);
    }
}
