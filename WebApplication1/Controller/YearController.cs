using Microsoft.AspNetCore.Mvc;
using WebApplication1.Bussiness.Dtos.Request;
using WebApplication1.Bussiness.Dtos.Responce;
using WebApplication1.Bussiness.Services.Interface;

namespace WebApplication1.Controller
{
    public class YearController : BaseController<YearController, IYearService, YearReq, YearRes>
    {
        public YearController(ILogger<YearController> logger, IYearService yearService) : base(logger, yearService)
        {

        }
    }
}
