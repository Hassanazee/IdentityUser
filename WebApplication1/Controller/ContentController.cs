using Microsoft.AspNetCore.Mvc;
using WebApplication1.Bussiness.Dtos.Request;
using WebApplication1.Bussiness.Dtos.Responce;
using WebApplication1.Bussiness.Services.Interface;

namespace WebApplication1.Controller
{
    public class ContentController : BaseController<ContentController, IContentService, ContentReq, ContentRes>
    {
        public ContentController(ILogger<ContentController> logger, IContentService contentService) : base(logger, contentService)
        {

        }

        /*[HttpGet]
        public async Task<IActionResult> Content(string Url)
        {
            var result = await Service.Content(Url);
            return result;
        }*/

    }
}