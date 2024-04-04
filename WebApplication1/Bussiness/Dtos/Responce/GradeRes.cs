using WebApplication1.Bussiness.Dtos.Request;
using WebApplication1.Entity;

namespace WebApplication1.Bussiness.Dtos.Responce
{
    public class GradeRes
    {
        public Guid GradeId { get; set; }
        public string GradeName { get; set; }
        public IList<StudentRes> Student { get; set; } = new List<StudentRes>();
        public IList<SubjectRes> Subjects { get; set; } = new List<SubjectRes>();

        public IList<YearRes> Year { get; set; } = new List<YearRes>();
        public OrganizationRes OrganizationRes { get; set; }

    }
}
