using Microsoft.AspNetCore.Mvc;
using WebApplication1.Bussiness.Dtos.Request;
using WebApplication1.Bussiness.Dtos.Responce;
using WebApplication1.Bussiness.Services.Implementation;
using WebApplication1.Bussiness.Services.Interface;

namespace WebApplication1.Controller
{

    public class GradeController : BaseController<GradeController, IGradeService, GradeReq, GradeRes>
    {
        public GradeController(ILogger<GradeController> logger, IGradeService gradeService) : base(logger, gradeService)
        {

        }


    

    }
}
