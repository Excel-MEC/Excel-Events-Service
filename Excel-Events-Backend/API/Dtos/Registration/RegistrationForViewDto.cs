using API.Models;

namespace API.Dtos.Registration
{
    public class RegistrationForViewDto
    {
        public int Id { get; set; }
        public int ExcelId { get; set; }
        public int EventId { get; set; }
        public Team Team { get; set; }
    }
}