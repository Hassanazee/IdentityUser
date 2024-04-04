using WebApplication1.Entity;

namespace WebApplication1.Bussiness.Dtos.Request
{
    public class StudentReq
    {
        public string Name { get; set; }
        public Guid? CurrentGradeId { get; set; }
        public Guid OrgId { get; set; }
        public Guid TeacherId { get; set; }
        public Guid RoomId { get; set; }
     //   public IList<SubjectStudent>? SubjectStudents { get; set; } = new List<SubjectStudent>();
    }
}
