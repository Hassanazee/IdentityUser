using WebApplication1.Data.Repo.Implementation;
using WebApplication1.Entity;

namespace WebApplication1.Bussiness.Dtos.Responce
{
    public class StudentRes
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid GradeId { get; set; }
        public Grade Grade { get; set; }
        public Guid OrgId { get; set; }
        public OrganizationRes Organization { get; set; }
        public Guid TeacherId { get; set; }
        public TeacherRes Teacher { get; set; }
        public Guid RoomId { get; set; }
        public RoomRes Room { get; set; }
        public IList<SubjectStudent>? SubjectStudents { get; set; }
        public IList<SubjectRes> subjects { get; set; }
    }
}
