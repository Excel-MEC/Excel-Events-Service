using API.Models.Custom;
using System;

namespace API.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int RoundId { get; set; }
        public string Round { get; set; }
        public int Day { get; set; }
        public DateTime Datetime { get; set; }
        public Event Event { get; set; }
    }
}