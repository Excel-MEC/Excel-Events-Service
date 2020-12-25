using System;

namespace API.Dtos.Schedule
{
    public class DataForScheduleDto
    {
        public int EventId { get; set; }
        public int RoundId { get; set; }
        public string Round { get; set; }
        public int Day { get; set; }
        public DateTime Datetime { get; set; }
    }
}