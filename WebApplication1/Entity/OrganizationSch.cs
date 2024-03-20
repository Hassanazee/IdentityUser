using WebApplication1.Entity.Base;

namespace WebApplication1.Entity
{
    public class OrganizationSch : GeneralBase
    {
        public string? Organization {  get; set; }
        public string Location { get; set; }
        public string TotalStaff {  get; set; }
        public string TotalStudent { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public IList<Student> Students { get; set; } = new List<Student>();
        public IList<Subject> Subjects { get; set; } = new List<Subject>();
    }
}
