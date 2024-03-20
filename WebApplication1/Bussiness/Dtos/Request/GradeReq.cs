using WebApplication1.Entity;

namespace WebApplication1.Bussiness.Dtos.Request
{
    public class GradeReq
    {
        public string? GradeName { get; set; }
        public string? Section { get; set; }
        public IList<StudentReq> Student { get; set; } 
        public IList<SubjectReq> Subjects { get; set; } 

    }
}
