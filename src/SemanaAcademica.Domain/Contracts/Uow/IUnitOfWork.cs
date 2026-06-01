using SemanaAcademica.Domain.Contracts.Infra;

namespace SemanaAcademica.Domain.Contracts.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IUserRepository Users { get; }

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<int> CommitAsync();
    }
}