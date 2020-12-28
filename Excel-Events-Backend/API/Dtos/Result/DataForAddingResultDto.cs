namespace API.Dtos
{
    public class DataForAddingResultDto
    { 
        public int EventId { get; set; }
        public int ExcelId { get; set; }
        public int TeamId { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
        public string TeamName { get; set; }
        public string TeamMembers { get; set; }
    }
}