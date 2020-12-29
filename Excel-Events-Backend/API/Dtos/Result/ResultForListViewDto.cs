using System.Collections.Generic;

namespace API.Dtos
{
    public class ResultForListViewDto
    {
        public int EventId { get; set; }
        public List<ResultForViewDto> Results { get; set; }
    }
}