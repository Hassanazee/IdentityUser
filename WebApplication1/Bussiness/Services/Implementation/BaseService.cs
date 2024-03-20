using Microsoft.AspNetCore.Mvc;
using WebApplication1.Bussiness.Services.Interface;
using WebApplication1.Data.Repo.Interface;
using WebApplication1.Data.UnitOfWork;
using WebApplication1.Entity.Base;

namespace WebApplication1.Bussiness.Services.Implementation
{
    public class BaseService<TReq, TRes, TRepository, T> : IBaseService<TReq, TRes>
     where TRepository : class, IBaseRepo<T> where T : IGeneralBase
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly TRepository Repository;

        protected BaseService(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            Repository = UnitOfWork.GetRepository<TRepository>();
        }

        public virtual async Task<IActionResult> GetAll(Pagination pagination)
        {
            try
            {   
                var (pag, data) = await Repository.GetAll(pagination);
                return (IActionResult)data;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Error",e);
            }
        }

        public virtual async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var entity = await Repository.Get(id);
                return entity.Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (IActionResult)e;
            }
        }

        public virtual async Task<IActionResult> Add(TReq reqModel)
        {
            try
            {
                var ss = await Repository.Add((T)(reqModel as IGeneralBase ??
                                              throw new InvalidOperationException(
                                                       "Conversion to IMinBase Failed. Make sure there's Id and CreatedDate properties.")));
                return (IActionResult)ss;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (IActionResult)e;
            }
        }

        public virtual async Task<IActionResult> Update(TReq reqModel)
        {
            try
            {
                var res = await Repository.Update((T)(reqModel as IGeneralBase ??
                throw new InvalidOperationException("Conversion to IMinBase Failed. Make sure there's Id and CreatedDate properties.")),null);

                return (IActionResult)res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return (IActionResult)e;
            }
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            try
            {
                var res = await Repository.Delete(id);
                return res;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }


    }
}
