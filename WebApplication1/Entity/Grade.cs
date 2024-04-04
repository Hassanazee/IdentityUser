using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Entity.Base;

namespace WebApplication1.Entity
{
    public class Grade : GeneralBase
    {
        public string? GradeName { get; set; }
        public string? Section { get; set; }
        public Year Year { get; set; }
        public IList<Student> Students { get; set; } = new List<Student>();
        public IList<Subject> Subjects { get; set; } = new List<Subject>();
        public Guid? OrganizationId { get; set; }
        public OrganizationSch Organization { get; set; }

    }
}
