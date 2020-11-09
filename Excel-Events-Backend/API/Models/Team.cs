using System.Collections.Generic;
using API.Dtos;

namespace API.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public ICollection<Registration> Registrations { get; set; }
    }
}