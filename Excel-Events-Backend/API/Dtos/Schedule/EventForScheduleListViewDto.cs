using System.Collections.Generic;

namespace API.Dtos.Schedule
{
    public class EventForScheduleListViewDto
    {
        public int Day { get; set; }
        public List<EventForScheduleViewDto> Events { get; set; }
    }
}