using System;
using API.Models.Custom;

namespace API.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public int CategoryId { get; set; }
        public string Category => Constants.Category[CategoryId];
        public int EventTypeId { get; set; }
        public string EventType => Constants.EventType[EventTypeId];
        public string About { get; set; }
        public string Venue { get; set; }
        public DateTime Datetime { get; set; }
        public int? EntryFee { get; set; }
        public int? PrizeMoney { get; set; }
        public int? EventHead1Id { get; set; }
        public EventHead EventHead1 { get; set; }
        public int? EventHead2Id { get; set; }
        public EventHead EventHead2 { get; set; }
        public bool IsTeam { get; set; }
        public int? TeamSize { get; set; }
        public int EventStatusId { get; set; }
        public string EventStatus => Constants.EventStatus[EventStatusId];
        public int? NumberOfRounds { get; set; }
        public int? CurrentRound { get; set; }
    }
}