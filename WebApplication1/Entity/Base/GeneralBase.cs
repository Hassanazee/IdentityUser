namespace WebApplication1.Entity.Base
{
    public class GeneralBase : IGeneralBase
    {
       
        public Guid Id { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedById { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public Guid? CreatedById { get; set; }
    }
}
