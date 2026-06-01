using SemanaAcademica.Application.Contracts;
using SemanaAcademica.Application.Model.Auth;
using SemanaAcademica.Application.Model.User;
using SemanaAcademica.CrossCutting.Security.Service;
using SemanaAcademica.Domain.Contracts.CrossCutting.Cryptography;
using SemanaAcademica.Domain.Contracts.Domain;
using SemanaAcademica.Domain.Entities;
using SemanaAcademica.Domain.Notifications;

namespace SemanaAcademica.Application.Services
{
    public class UserApplicationService : IUserApplicationService
    {
        private readonly NotificationContext _notificationContext;
        private readonly ICryptoghaphy _cryptography;
        private readonly IUserDomainService _userDomainService;
        private readonly AccessTokenService _accessTokenService;

        public UserApplicationService(
            NotificationContext notificationContext,
            ICryptoghaphy cryptography,
            IUserDomainService userDomainService,
            AccessTokenService accessTokenService)
        {
            _notificationContext = notificationContext;
            _cryptography = cryptography;
            _userDomainService = userDomainService;
            _accessTokenService = accessTokenService;
        }

        /// <summary>
        /// Cria um novo usuário no sistema com a senha encriptografada
        /// </summary>
        public async Task<bool> CreateAsync(UserModel model)
        {
            // Valida a senha antes de encriptar
            if (model.Password != model.ConfirmPassword)
            {
                _notificationContext.AddNotification("Password", "As senhas não conferem.");
                return false;
            }

            if (!IsValidPassword(model.Password))
                return false;

            var emailExists = await _userDomainService.ExistsByEmailAsync(model.Email);
            if (emailExists)
            {
                _notificationContext.AddNotification("Email", "Já existe um usuário com este e-mail.");
                return false;
            }

            var encryptedPassword = _cryptography.Encrypt(model.Password);
            var user = new UserEntity(model.FullName, model.Email, encryptedPassword);

            await _userDomainService.AddAsync(user);

            return !_notificationContext.HasNotifications;
        }

        /// <summary>
        /// Realiza a autenticação do usuário e retorna o token de acesso
        /// </summary>
        public async Task<string?> GetAccessAsync(AccessModel model)
        {
            var encryptedPassword = _cryptography.Encrypt(model.Password);

            var user = await _userDomainService.GetByEmailAndPasswordAsync(model.Email, encryptedPassword);

            if (user is null)
                return null;

            return _accessTokenService.GenerateToken(user.Email);
        }

        /// <summary>
        /// Retorna todos os usuários cadastrados
        /// </summary>
        public async Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            return await _userDomainService.GetAllAsync();
        }

        /// <summary>
        /// Retorna um usuário pelo Id
        /// </summary>
        public async Task<UserEntity?> GetByIdAsync(Guid id)
        {
            return await _userDomainService.GetByIdAsync(id);
        }

        /// <summary>
        /// Remove um usuário pelo Id
        /// </summary>
        public async Task<bool> DeleteAsync(Guid id)
        {
            await _userDomainService.DeleteAsync(id);
            return !_notificationContext.HasNotifications;
        }

        private bool IsValidPassword(string password)
        {
            if (password.Length < 8)
            {
                _notificationContext.AddNotification("Password", "A senha deve ter no mínimo 8 caracteres.");
                return false;
            }
            if (!password.Any(char.IsUpper))
            {
                _notificationContext.AddNotification("Password", "A senha deve conter ao menos uma letra maiúscula.");
                return false;
            }
            if (!password.Any(char.IsLower))
            {
                _notificationContext.AddNotification("Password", "A senha deve conter ao menos uma letra minúscula.");
                return false;
            }
            if (!password.Any(char.IsDigit))
            {
                _notificationContext.AddNotification("Password", "A senha deve conter ao menos um número.");
                return false;
            }
            if (password.All(char.IsLetterOrDigit))
            {
                _notificationContext.AddNotification("Password", "A senha deve conter ao menos um caractere especial.");
                return false;
            }

            return true;
        }
    }
}