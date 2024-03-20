using Microsoft.AspNetCore.Mvc;
using WebApplication1.Bussiness.Dtos.Request;
using WebApplication1.Bussiness.Dtos.Responce;
using WebApplication1.Bussiness.Services.Interface;

namespace WebApplication1.Controller
{
    public class TeacherController : BaseController<TeacherController, ITeacherService, TeacherReq, TeacherRes>
    {
        public TeacherController(ILogger<TeacherController> logger, ITeacherService teacherService) : base(logger, teacherService)
        {

        }
    }

}
