using SemanaAcademica.Application.Model.Customer;

namespace SemanaAcademica.Application.Contracts
{
    public interface ICustomerApplicationService
    {
        Task<CustomerModel?> GetByIdAsync(Guid id);
        Task<IEnumerable<CustomerModel>> GetAllAsync();
        Task<CustomerModel> AddAsync(CustomerModel customerDto);
        Task<CustomerModel> UpdateAsync(Guid Id, CustomerModel customerDto);
        Task DeleteAsync(Guid id);
    }
}
