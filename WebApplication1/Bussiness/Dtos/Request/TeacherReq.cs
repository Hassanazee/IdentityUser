namespace WebApplication1.Bussiness.Dtos.Request
{
    public class TeacherReq
    {
        public string Name { get; set; }
        public string Salary { get; set; }
        public string SubjectTitle { get; set; }
        public Guid OrgId { get; set; }
        public IList<StudentReq> Students { get; set; } = new List<StudentReq>();

    }
}
