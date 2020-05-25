namespace API.Models
{
    public class Registration
    {    
        public int Id { get; set; }
        public int ExcelId { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }  
    }
}