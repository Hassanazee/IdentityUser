namespace WebApplication1.Bussiness.Dtos.Request
{
    public class RoomReq
    {
        public string ClassName { get; set; }
        public string Location { get; set; }
        public int TotalStaff { get; set; }
        public int TotalStudent { get; set; }
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }
        public Guid OrgId { get; set; }
    }
}
