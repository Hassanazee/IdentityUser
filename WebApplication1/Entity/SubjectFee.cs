using WebApplication1.Entity.Base;

namespace WebApplication1.Entity
{
    public class SubjectFee : GeneralBase
    {
        public string Fee { get; set; }
        public Subject Subject { get; set; }

    }
}
