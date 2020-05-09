using Microsoft.AspNetCore.Http;

namespace API.Dtos.Event
{
    public class DataForDeletingEventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}