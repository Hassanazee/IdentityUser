using WebApplication1.Entity.Base;

namespace WebApplication1.Entity
{
    public class SubjectStudent 
    {
        public Guid? StudentId { get; set; }
        public Student Student { get; set; }

        public Guid? SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
