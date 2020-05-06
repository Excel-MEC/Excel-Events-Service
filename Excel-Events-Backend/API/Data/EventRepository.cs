using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Event;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class EventRepository : IEventRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public EventRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EventForListViewDto>> EventList()
        {
            List<EventForListViewDto> events = await _context.Events.Select(e => _mapper.Map<EventForListViewDto>(e)).ToListAsync();
            return events;
        }

        public async Task<Event> GetEvent(int id)
        {
            Event eventFromdb = await _context.Events.Include(e => e.EventHead1).Include(e => e.EventHead2).FirstOrDefaultAsync(e => e.Id == id);
            return eventFromdb;
        }
    }
}