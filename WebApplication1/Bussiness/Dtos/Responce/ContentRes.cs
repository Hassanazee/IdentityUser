using WebApplication1.Entity;

namespace WebApplication1.Bussiness.Dtos.Responce
{
    public class ContentRes
    {
        public string? Name { get; set; }
        public string? URL { get; set; }
        public Guid? SubjectId { get; set; }
        public IList<SubjectRes>? SubjectDetails { get; set; }
    }
}
