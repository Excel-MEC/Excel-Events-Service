using Microsoft.AspNetCore.Http;

namespace API.Dtos.Highlight
{
    public class DataForAddingHighlightDto
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}