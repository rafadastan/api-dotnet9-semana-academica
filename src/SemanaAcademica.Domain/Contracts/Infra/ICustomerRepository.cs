using SemanaAcademica.Domain.Entities;

namespace SemanaAcademica.Domain.Contracts.Infra
{
    public interface ICustomerRepository : IBaseRepository<CustomerEntity>
    {
        Task<bool> ExistsByCpfAsync(string cpf);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
