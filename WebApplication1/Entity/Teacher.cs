using WebApplication1.Entity.Base;

namespace WebApplication1.Entity
{
    public class Teacher : GeneralBase
    {
        public string? Name { get; set; }
        public string Salary { get; set; }
        public string SubjectTitle { get; set; }

        public Guid OrgId { get; set; }
        public OrganizationSch Organization { get; set; }
        public IList<Student> Student { get; set; } = new List<Student>();
    }
}
