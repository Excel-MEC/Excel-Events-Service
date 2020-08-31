using System;
using API.Models.Custom;

namespace API.Dtos.Schedule
{
    public class ScheduleForViewDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int RoundId { get; set; }
        public string Round => Constants.EventRounds[RoundId];
        public int Day { get; set; }
        public DateTime Datetime { get; set; }
    }
}