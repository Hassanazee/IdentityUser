using WebApplication1.Entity.Base;

namespace WebApplication1.Data.Repo.Interface
{
    public interface IBaseRepo<TEntity> : IDisposable where TEntity : IGeneralBase
    {
        public Task<(Pagination, IList<TEntity>)> GetAll(Pagination pagination,
           Func<IQueryable<TEntity>, IQueryable<TEntity>>? func = null);
        public Task<TEntity> Get(Guid id);

       public  Task<TEntity> Add(TEntity model);

       public  Task<TEntity> Update(TEntity model, Func<TEntity, TEntity>? func);

       public  Task<bool> Delete(Guid id);
    }
}