using WebApplication1.Entity;

namespace WebApplication1.Bussiness.Dtos.Request
{
    public class SubjectReq
    {
        public Guid Id { get; set; }
        public string SubjectName { get; set; }
        public Guid GradeId { get; set; }
       // public IList<GradeReq> Grades {get; set; } = new List<GradeReq>();
        //public IList<SubjectStudent>? SubjectStudents { get; set; } = new List<SubjectStudent>();

    }
}
