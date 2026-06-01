using FluentValidation;
using SemanaAcademica.Domain.Contracts.Domain;
using SemanaAcademica.Domain.Contracts.Infra;
using SemanaAcademica.Domain.Entities;
using SemanaAcademica.Domain.Notifications;

namespace SemanaAcademica.Domain.Services
{
    public class UserDomainService : BaseDomainService<UserEntity>, IUserDomainService
    {
        private readonly IUserRepository _userRepository;

        public UserDomainService(
            IBaseRepository<UserEntity> baseRepository,
            NotificationContext notificationContext,
            IValidator<UserEntity> userValidator,
            IUserRepository userRepository)
            : base(baseRepository, notificationContext, userValidator)
        {
            _userRepository = userRepository;
        }

        public override Task<UserEntity?> GetByIdAsync(Guid id)
        {
            return _userRepository.GetByIdAsync(id);
        }

        public override Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return _userRepository.GetAllAsync();
        }

        public async Task<bool> ExistsByEmailAsync(string email)
        {
            return await _userRepository.ExistsByEmailAsync(email);
        }

        public async Task<UserEntity?> GetByEmailAndPasswordAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAndPasswordAsync(email, password);

            if (user is null)
                _notificationContext.AddNotification("Auth", "E-mail ou senha inválidos.");

            return user;
        }
    }
}