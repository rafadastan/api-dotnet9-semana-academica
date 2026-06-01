using Microsoft.EntityFrameworkCore.Storage;
using SemanaAcademica.Domain.Contracts.Infra;
using SemanaAcademica.Domain.Contracts.Uow;
using SemanaAcademica.Infra.Context;

namespace SemanaAcademica.Infra.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(DataContext context,
                          ICustomerRepository customerRepository,
                          IUserRepository userRepository)
        {
            _context = context;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        public ICustomerRepository Customers => _customerRepository;
        public IUserRepository Users => _userRepository;

        public async Task BeginTransactionAsync()
        {
            if (_transaction == null)
                _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            _transaction?.Dispose();
        }
    }
}