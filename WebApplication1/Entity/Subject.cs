using WebApplication1.Entity.Base;

namespace WebApplication1.Entity
{
    public class Subject : GeneralBase
    {
        public string SubjectName { get; set; }
        public Guid GradeId { get; set; }
        public Grade Grade { get; set; }
        public IList<SubjectStudent>? SubjectStudents { get; set; }
        public IList<Content>? Contents { get; set; } = new List<Content>();
        public Guid FeeId { get; set; }
        public SubjectFee SubjectFee { get; set; }
        public Guid OrgId { get; set; }
        public OrganizationSch Organization { get; set; }

        //One to one
        public Guid RoomId { get; set; }
        public Room Room { get; set; }


        public IList<TimeTableEntry> TimeTableEntries { get; set; } = new List<TimeTableEntry>();

    }
}
