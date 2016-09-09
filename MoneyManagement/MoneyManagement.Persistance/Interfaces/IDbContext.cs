using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace MoneyManagement.Persistance.Interfaces
{
    public interface IDbContext : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
