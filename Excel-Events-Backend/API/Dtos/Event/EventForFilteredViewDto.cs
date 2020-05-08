using System;

namespace API.Dtos.Event
{
    public class EventForFilteredViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int EventTypeId { get; set; }
        public int CategoryId { get; set; }
        public DateTime Datetime { get; set; }
    }
}