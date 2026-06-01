using Microsoft.EntityFrameworkCore;
using SemanaAcademica.Domain.Contracts.Infra;
using SemanaAcademica.Domain.Entities;
using SemanaAcademica.Infra.Context;

namespace SemanaAcademica.Infra.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public Task<bool> ExistsByEmailAsync(string email) =>
            _dbSet.AnyAsync(u => u.Email == email);

        public Task<UserEntity?> GetByEmailAndPasswordAsync(string email, string password) =>
            _dbSet.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

        public override async Task<UserEntity?> GetByIdAsync(Guid id)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public override async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await _dbSet
                .ToListAsync();
        }
    }
}