using System;

namespace API.Dtos.Event
{
    public class DataForAddingEventRoundDto
    {
        public int EventId { get; set; }
        public int RoundId { get; set; }
        public int Day { get; set; }
        public DateTime Datetime { get; set; }
    }
}