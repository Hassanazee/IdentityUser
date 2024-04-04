using WebApplication1.Entity.Base;

namespace WebApplication1.Entity
{
    public class TimeTable : GeneralBase
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public  IList<TimeTableEntry> TimeTableEntries { get; set; } = new List<TimeTableEntry>();
    }
}
