using System.Collections.Generic;
using API.Dtos.Registration;
using API.Models;

namespace API.Dtos.Teams
{
    public class TeamForViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EventId { get; set; }
        public List<UserForViewDto> Members { get; set; }
    }
}