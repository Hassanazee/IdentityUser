using WebApplication1.Bussiness.Dtos.Request;
using WebApplication1.Entity;

namespace WebApplication1.Bussiness.Dtos.Responce
{
    public class SubjectRes
    {
        public Guid Id { get; set; }
        public string SubjectName { get; set; }
        public Guid GradeId { get; set; }
        public IList<Grade> Grades { get; set; } = new List<Grade>();

 //       public IList<SubjectStudent> SubjectStudents { get; set; } = new List<SubjectStudent>();
    }
}
