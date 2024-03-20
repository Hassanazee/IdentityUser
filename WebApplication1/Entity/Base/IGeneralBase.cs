namespace WebApplication1.Entity.Base
{
    public interface IGeneralBase
    {
        public void GeneralBase()
        {
            IsDelete = false;
        }
        
        public bool IsDelete { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid? ModifiedById { get; set; }
    }
}
