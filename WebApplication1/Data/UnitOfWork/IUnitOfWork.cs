using Microsoft.EntityFrameworkCore.Storage;

namespace WebApplication1.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync(IDbContextTransaction? transaction);
        Task RollBackTransactionAsync(IDbContextTransaction? transaction);
        DataContext Context { get; }
        T GetRepository<T>() where T : class;
    }
}
