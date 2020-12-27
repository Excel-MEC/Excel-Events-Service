using System;
using System.Collections.Generic;
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
        public string Format { get; set; }
        public string Rules { get; set; }
        public string Venue { get; set; }
        public int? Day { get; set; }
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
        public ICollection<Registration> Registrations { get; set; }
        public ICollection<Bookmark> Bookmarks { get; set; }
        public bool? NeedRegistration { get; set; }
        public bool? RegistrationOpen { get; set; }
        public DateTime? RegistrationEndDate { get; set; }
        public string Button { get; set; }
        public string RegistrationLink { get; set; }
        public ICollection<Schedule> Rounds { get; set; }
        public ICollection<Team> Teams { get; set; }
        public ICollection<Result> Results { get; set; }
    }
}