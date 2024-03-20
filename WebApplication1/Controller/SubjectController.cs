using Microsoft.AspNetCore.Mvc;
using WebApplication1.Bussiness.Dtos.Request;
using WebApplication1.Bussiness.Dtos.Responce;
using WebApplication1.Bussiness.Services.Interface;

namespace WebApplication1.Controller
{
    public class SubjectController : BaseController<SubjectController , ISubjectService , SubjectReq , SubjectRes>
    {
        public SubjectController(ILogger<SubjectController> logger , ISubjectService subjectService): base(logger , subjectService) 
        {
            
        }
    }
}
