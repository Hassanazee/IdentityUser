using WebApplication1.Entity.Base;

namespace WebApplication1.Entity
{
    public class Student : GeneralBase
    {
        public string Name { get; set; }
        public Guid? CurrentGradeId { get; set; }
        public Grade CurrentGrade { get; set; }
        public IList<SubjectStudent> SubjectStudents { get; set; }   
        public Guid OrgId { get; set; }
        public OrganizationSch Organization { get; set; }
        public Guid teacherId { get; set; }
        public Teacher Teacher { get; set; }
        public Guid RoomId { get; set; }
        public Room Room { get; set; }
    }
}
