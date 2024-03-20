using WebApplication1.Entity.Base;

namespace WebApplication1.Entity
{
    public class Content : GeneralBase
    {
        public string? Name { get; set; }
        public string? URL { get; set; }
        public Guid? SubjectId { get; set; }
        public Subject? Subject { get; set; }

        public string GenerateUrlCode()
        {

            if (SubjectId.HasValue)
            {
                string subjectIdentifier = SubjectId.ToString();
                string urlCode = $"{subjectIdentifier}/{URL}";
                return urlCode;
            }
            else
            {
                throw new InvalidOperationException("SubjectId cannot be null when generating URL code.");
            }
        }

    }
}
