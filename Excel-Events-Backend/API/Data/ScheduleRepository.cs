using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using API.Data.Interfaces;
using API.Dtos.Schedule;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ScheduleRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<EventForScheduleListViewDto>> ScheduleList()
        {
            List<EventForScheduleListViewDto> eventForScheduleList = new List<EventForScheduleListViewDto>();
            var eventList = await _context.Rounds.Include(r => r.Event).Select(q => _mapper.Map<EventRoundForScheduleViewDto>(q)).ToListAsync();
            var events = eventList.GroupBy(x => x.Day)
                              .Select(g => new { g.Key, Events = g.OrderBy(x => x.Datetime).ToList() })
                              .OrderBy(x => x.Key)
                              .ToList();
            foreach (var group in events)
            {
                var eventForSchedule = new EventForScheduleListViewDto();
                var eventListForView = Map(group.Events);
                eventForSchedule.Day = group.Key;
                eventForSchedule.Events = eventListForView;
                eventForScheduleList.Add(eventForSchedule);
            }
            return eventForScheduleList;
        }
        private List<EventForScheduleViewDto> Map(List<EventRoundForScheduleViewDto> events)
        {
            var eventList = new List<EventForScheduleViewDto>();
            foreach (var e in events)
            {
                var eventForView = new EventForScheduleViewDto
                {
                    Id = e.Event.Id,
                    Name = e.Event.Name,
                    Icon = e.Event.Icon,
                    EventType = e.Event.EventType,
                    Category = e.Event.Category,
                    Round = e.Round,
                    Datetime = e.Datetime,
                    Day = e.Day
                };
                eventList.Add(eventForView);
            }
            return eventList;
        }
    }
}