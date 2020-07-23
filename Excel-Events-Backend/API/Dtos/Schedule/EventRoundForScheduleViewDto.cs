using System;
using API.Dtos.Event;

namespace API.Dtos.Schedule
{
    public class EventRoundForScheduleViewDto
    {
        public EventForListViewDto Event { get; set; }
        public string Round { get; set; }
        public int Day { get; set; }
        public DateTime Datetime { get; set; }
    }
}