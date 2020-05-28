using System.Collections.Generic;
using API.Dtos.Event;
namespace API.Dtos.Schedule
{
    public class EventForScheduleListViewDto
    {
        public int Day { get; set; }
        public List<EventForListViewDto> Events { get; set; }
    }
}