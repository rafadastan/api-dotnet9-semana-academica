using SemanaAcademica.Domain.Entities;

namespace SemanaAcademica.Domain.Contracts.Infra
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        /// <summary>
        /// Verifica se já existe um usuário com o e-mail informado
        /// </summary>
        Task<bool> ExistsByEmailAsync(string email);

        /// <summary>
        /// Busca usuário pelo e-mail e senha para autenticação
        /// </summary>
        Task<UserEntity?> GetByEmailAndPasswordAsync(string email, string password);
    }
}
