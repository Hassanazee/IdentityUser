namespace WebApplication1.Bussiness.Dtos.Request
{
    public class OrganizationReq
    {
        public string Organization { get; set; }
        public string Location { get; set; }
        public string TotalStaff { get; set; }
        public string TotalStudent { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public IList<StudentReq> Students { get; set; } = new List<StudentReq>();
        public IList<SubjectReq> Subjects { get; set; } = new List<SubjectReq>();
    }
}
