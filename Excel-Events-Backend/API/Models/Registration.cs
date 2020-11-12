using System.Collections.Generic;

namespace API.Models
{
    public class Registration
    {    
        public int Id { get; set; }
        public int ExcelId { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }  
        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }
}