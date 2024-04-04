using WebApplication1.Entity.Base;

namespace WebApplication1.Entity
{
    public class Room : GeneralBase
    {
        public string ClassName { get; set; }
        public string Location { get; set; }
        public int TotalStaff { get; set; }
        public int TotalStudent { get; set; }
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }
       public Guid OrgId { get; set; }
       public OrganizationSch Organization { get; set; }
       public Subject Subject { get; set; }
       public  IList<TimeTableEntry> TimeTableEntries { get; set; } = new List<TimeTableEntry>();

    }
}
