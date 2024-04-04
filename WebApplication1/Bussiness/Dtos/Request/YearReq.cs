namespace WebApplication1.Bussiness.Dtos.Request
{
    public class YearReq
    {
        public Guid YearId { get; set; }
        public string GradeName { get; set; }
        public DateTime EnrollmentYear { get; set; }
        public DateTime EndYear { get; set; }
    }
}
