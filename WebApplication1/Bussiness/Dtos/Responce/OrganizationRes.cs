namespace WebApplication1.Bussiness.Dtos.Responce
{
    public class OrganizationRes
    {
        public Guid Id { get; set; }
        public string Organization { get; set; }
        public string Location { get; set; }
        public string TotalStaff { get; set; }
        public string TotalStudent { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public IList<StudentRes> Students { get; set; } = new List<StudentRes>();
        public IList<SubjectRes> Subjects { get; set; } = new List<SubjectRes>();

    }
}
