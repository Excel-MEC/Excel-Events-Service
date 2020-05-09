using System;
using Microsoft.AspNetCore.Http;

namespace API.Dtos.Event
{
    public class DataForUpdatingEventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Icon { get; set; }
        public int? CategoryId { get; set; }
        public int? EventTypeId { get; set; }
        public string About { get; set; }
        public string Format { get; set; }
        public string Rules { get; set; }
        public string Venue { get; set; }
        public DateTime Datetime { get; set; }
        public int? EntryFee { get; set; }
        public int? PrizeMoney { get; set; }
        public int? EventHead1Id { get; set; }
        public int? EventHead2Id { get; set; }
        public bool IsTeam { get; set; }
        public int? TeamSize { get; set; }
        public int? EventStatusId { get; set; }
        public int? NumberOfRounds { get; set; }
        public int? CurrentRound { get; set; }
    }
}