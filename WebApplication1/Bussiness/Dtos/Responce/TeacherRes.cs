namespace WebApplication1.Bussiness.Dtos.Responce
{
    public class TeacherRes
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Salary { get; set; }
        public string SubjectTitle { get; set; }
        public Guid OrgId { get; set; }
        public IList<StudentRes> Students { get; set; } = new List<StudentRes>();

    }
}
