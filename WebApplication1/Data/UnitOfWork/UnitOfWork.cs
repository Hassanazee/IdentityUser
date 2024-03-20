using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace WebApplication1.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
        {
       
            public UnitOfWork(DataContext context)
            {
                Context = context;
            }

            public DataContext Context { get; }

            public T GetRepository<T>() where T : class
            {
                var result = Activator.CreateInstance(typeof(T), Context);
                return result as T ?? throw new InvalidOperationException("This Error shouldn't Arise!");
            }

            public async Task<IDbContextTransaction> BeginTransactionAsync()
            {
                return await Context.Database.BeginTransactionAsync();
            }

            public async Task CommitTransactionAsync(IDbContextTransaction? transaction)
            {
                if (transaction != null)
                {
                    await transaction.CommitAsync();
                }
            }

            public async Task RollBackTransactionAsync(IDbContextTransaction? transaction)
            {
                if (transaction != null)
                {
                    await transaction.RollbackAsync();
                }
            }

            public async Task SaveAsync()
            {
                await Context.SaveChangesAsync();
            }

            private bool _disposed;

            private void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    if (disposing)
                    {
                        Context.Dispose();
                    }
                }

                _disposed = true;
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }
    }
