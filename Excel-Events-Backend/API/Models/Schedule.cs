using API.Models.Custom;
using System;

namespace API.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int RoundId { get; set; }
        public string Round => Constants.EventRounds[RoundId];
        public int Day { get; set; }
        public DateTime Datetime { get; set; }
        public Event Event { get; set; }
    }
}