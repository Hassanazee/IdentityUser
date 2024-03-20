using WebApplication1.Entity.Base;

namespace WebApplication1.Entity
{
    public class TimeTableEntry : GeneralBase
    {
        public Guid TimetableId { get; set; }
        public Guid ClassId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid TeacherId { get; set; }
        public Guid RoomId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
