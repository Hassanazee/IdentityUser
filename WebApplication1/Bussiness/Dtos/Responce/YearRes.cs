namespace WebApplication1.Bussiness.Dtos.Responce
{
    public class YearRes
    {
        public Guid Id { get; set; }
        public string GradeName { get; set; }
        public DateTime EnrollmentYear { get; set; }
        public DateTime EndYear { get; set; }
        public IList<GradeRes> Grades { get; set; } = new List<GradeRes>();

    }
}
