using SemanaAcademica.Application.Model.Auth;
using SemanaAcademica.Application.Model.User;
using SemanaAcademica.Domain.Entities;

namespace SemanaAcademica.Application.Contracts
{
    public interface IUserApplicationService
    {
        Task<bool> CreateAsync(UserModel model);
        Task<string?> GetAccessAsync(AccessModel model);
        Task<IEnumerable<UserEntity>> GetAllAsync();
        Task<UserEntity?> GetByIdAsync(Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}