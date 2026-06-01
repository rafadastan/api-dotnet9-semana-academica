using SemanaAcademica.Domain.Entities;

namespace SemanaAcademica.Domain.Contracts.Domain
{
    public interface ICustomerDomainService : IBaseDomainService<CustomerEntity>
    {
        Task<bool> ExistsByCpfAsync(string cpf);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
