using System.Collections.Generic;

namespace API.Models
{
    public class Result
    {    
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
        public string TeamName { get; set; }
        public string TeamMembers { get; set; }
    }
}