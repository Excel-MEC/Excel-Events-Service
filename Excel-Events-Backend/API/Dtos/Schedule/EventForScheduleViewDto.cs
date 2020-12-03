using System;
namespace API.Dtos.Schedule
{
    public class EventForScheduleViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string EventType { get; set; }
        public string Category { get; set; }
        public string Venue { get; set; }
        public bool? NeedRegistration { get; set; }
        public string Round { get; set; }
        public int Day { get; set; }
        public DateTime Datetime { get; set; }
    }
}