namespace WebApplication1.Bussiness.Dtos.Responce
{
    public class RoomRes
    {
        public Guid Id { get; set; }
        public string ClassName { get; set; }
        public string Location { get; set; }
        public int TotalStaff { get; set; }
        public int TotalStudent { get; set; }
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }
        public Guid OrgId { get; set; }
        public IList<OrganizationRes> Organization { get; set; } = new List<OrganizationRes>();
    }
}
