using WebApplication1.Entity.Base;

namespace WebApplication1.Entity
{
    public class Year : GeneralBase
    {
        public string GradeName { get; set; }

        public DateTime EnrollmentYear { get; set; }
        public DateTime EndYear { get; set; }
        public IList<Grade> Grade { get; set; } = new List<Grade>();
    }
}
