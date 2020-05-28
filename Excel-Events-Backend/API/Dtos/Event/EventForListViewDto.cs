using System;

namespace API.Dtos.Event
{
    public class EventForListViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string EventType { get; set; }
        public string Category { get; set; }
        public string Day { get; set; }
        public DateTime Datetime { get; set; }
    }
}