using FluentValidation;
using FluentValidation.Results;
using SemanaAcademica.Domain.Contracts.Domain;
using SemanaAcademica.Domain.Contracts.Infra;
using SemanaAcademica.Domain.Notifications;

namespace SemanaAcademica.Domain.Services
{
    public abstract class BaseDomainService<TEntity> : IBaseDomainService<TEntity> where TEntity : class
    {
        protected readonly IBaseRepository<TEntity> _repository;
        protected readonly NotificationContext _notificationContext;
        protected readonly IValidator<TEntity>? _validator;

        protected BaseDomainService(
            IBaseRepository<TEntity> repository,
            NotificationContext notificationContext,
            IValidator<TEntity>? validator = null)
        {
            _repository = repository;
            _notificationContext = notificationContext;
            _validator = validator;
        }

        protected bool ValidateEntity(TEntity entity)
        {
            if (_validator == null) return true;

            ValidationResult result = _validator.Validate(entity);

            if (!result.IsValid)
            {
                _notificationContext.AddNotifications(result);
                return false;
            }
            return true;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                _notificationContext.AddNotification("NotFound", $"Registro com id {id} não encontrado.");

            return entity;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            if (!ValidateEntity(entity))
                return;

            await _repository.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            if (!ValidateEntity(entity))
                return;

            await _repository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                _notificationContext.AddNotification("NotFound", $"Registro com id {id} não encontrado.");
                return;
            }

            await _repository.RemoveAsync(entity);
        }
    }
}
