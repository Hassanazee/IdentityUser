using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Bussiness.Services.Interface
{
    public interface IBaseService<in TReq, out TRes>
    {
        public Task<IActionResult> GetAll(Pagination pagination);
        public Task<IActionResult> Get(Guid id);
        public Task<IActionResult> Add(TReq reqModel);
        public Task<IActionResult> Update(TReq reqModel);
        public Task<bool> Delete(Guid id);

    }
}
