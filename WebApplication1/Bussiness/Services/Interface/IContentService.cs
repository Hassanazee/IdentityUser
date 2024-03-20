using Microsoft.AspNetCore.Mvc;
using WebApplication1.Bussiness.Dtos.Request;
using WebApplication1.Bussiness.Dtos.Responce;
using WebApplication1.Entity;

namespace WebApplication1.Bussiness.Services.Interface
{
    public interface IContentService : IBaseService<ContentReq , ContentRes>
    {
       public Task<IActionResult> Content(string Url);

    }



}
