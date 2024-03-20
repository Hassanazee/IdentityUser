using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Repo.Interface;
using WebApplication1.Entity.Base;

namespace WebApplication1.Data.Repo.Implementation
{
    public abstract class BaseRepo<TEntity> : IBaseRepo<TEntity> where TEntity : class, IGeneralBase
    {
        protected readonly DbSet<TEntity> DbSet;
        protected readonly DbContext DbContext;
        protected BaseRepo(DbContext dbContext )
        {
            DbSet = dbContext.Set<TEntity>();
            DbContext = dbContext;
            
        }

        public virtual async Task<TEntity> Add(TEntity model)
        {
            await DbSet.AddAsync(model);
            await DbContext.SaveChangesAsync();
            return model;
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            var found = await DbSet.FindAsync(id);
            if (found == null)
            {
                throw new ($"{nameof(TEntity)} not found for Id:{id}");
            }

            DbSet.Remove(found);
            await DbContext.SaveChangesAsync();
            return true;
        }

        public virtual async Task<TEntity> Get(Guid id)
        {
            var data = await DbSet.FindAsync(id);
            return data ?? throw new ($"{nameof(TEntity)} not found for Id:{id}");
        }

        public virtual async Task<(Pagination, IList<TEntity>)> GetAll(Pagination pagination,
            Func<IQueryable<TEntity>, IQueryable<TEntity>>? includeFunc)
        {
            var total = 0;
            var totalPages = 0;

            var res = await (includeFunc?.Invoke(DbSet) ?? DbSet).Where(f => f.IsDelete != true)
                .Paginate(pagination.PageIndex, pagination.PageSize, ref total, ref totalPages).ToListAsync();

            pagination = pagination.Combine(total, totalPages);
            return (pagination, res);
        }
        public virtual async Task<TEntity> Update(TEntity model, Func<TEntity, TEntity>? func = null)
        {
            var found = await DbSet.FindAsync(model.GeneralBase);
            if (found == null)
            {
                throw new ($"{nameof(TEntity)} not found with Id: {model.GeneralBase}");
            }

            if (func != null)
            {
                model = func(found);
            }

            DbSet.Update(model);
            await DbContext.SaveChangesAsync();

            return model;
        }

        public void Dispose()
        {
            DbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
