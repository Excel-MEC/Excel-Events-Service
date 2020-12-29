using System.Collections.Generic;

namespace API.Dtos
{
    public class ResultForListViewDto
    {
        public bool isTeam { get; set; }
        public List<ResultForViewDto> Results { get; set; }
    }
}