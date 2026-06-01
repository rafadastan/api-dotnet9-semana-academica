using Microsoft.EntityFrameworkCore;
using SemanaAcademica.Domain.Entities;

namespace SemanaAcademica.Infra.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<CustomerEntity> Customer { get; set; }
        public DbSet<UserEntity> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}