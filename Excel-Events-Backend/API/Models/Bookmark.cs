namespace API.Models
{
    public class Bookmark
    {
        public int Id { get; set; }
        public int ExcelId { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public bool IsRegistered { get; set; }
    }
}