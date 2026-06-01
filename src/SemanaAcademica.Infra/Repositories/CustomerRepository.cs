using Microsoft.EntityFrameworkCore;
using SemanaAcademica.Domain.Contracts.Infra;
using SemanaAcademica.Domain.Entities;
using SemanaAcademica.Infra.Context;

namespace SemanaAcademica.Infra.Repositories
{
    public class CustomerRepository : BaseRepository<CustomerEntity>, ICustomerRepository
    {
        public CustomerRepository(DataContext context) : base(context)
        {
        }

        public Task<bool> ExistsByEmailAsync(string email) =>
            _dbSet.AnyAsync(c => c.Email == email);

        public override async Task<CustomerEntity?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public override async Task<IEnumerable<CustomerEntity>> GetAllAsync()
        {
            return await _dbSet
                .ToListAsync();
        }

        public Task<bool> ExistsByCpfAsync(string cpf) =>
            _dbSet.AnyAsync(c => c.Cpf == cpf);
    }
}
