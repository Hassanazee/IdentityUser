namespace WebApplication1.Bussiness.Dtos.Responce
{
    public class SubjectStudentRes
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public SubjectRes Subject { get; set; }
        public StudentRes Student { get; set; }

    }
}
