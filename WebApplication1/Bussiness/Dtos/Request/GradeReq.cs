using WebApplication1.Entity;

namespace WebApplication1.Bussiness.Dtos.Request
{
    public class GradeReq
    {
        public string? GradeName { get; set; }
        public string? Section { get; set; }
       // public IList<StudentReq> Student { get; set; } = new List<StudentReq>();
        public IList<SubjectReq> Subjects { get; set; } = new List<SubjectReq>();
        public YearReq Year { get; set; } = null;
        public Guid OrgId { get; set; }

    }
}
