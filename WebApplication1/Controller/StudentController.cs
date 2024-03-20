using Microsoft.AspNetCore.Mvc;
using WebApplication1.Bussiness.Dtos.Request;
using WebApplication1.Bussiness.Dtos.Responce;
using WebApplication1.Bussiness.Services.Interface;
using WebApplication1.Controller;
namespace WebApplication1.Controllers
{

    public class StudentController : BaseController<StudentController, IStudentService, StudentReq, StudentRes>
    {
        public StudentController(ILogger<StudentController> logger, IStudentService studentService) : base(logger, studentService)
        {

        }
        
    }
}
