using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using API.Data.Interfaces;
using API.Dtos.Event;
using API.Dtos.Schedule;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

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

        public async Task<List<EventForScheduleListViewDto>> EventList()
        {
            List<EventForScheduleListViewDto> eventForScheduleList = new List<EventForScheduleListViewDto>();
            var query = await _context.Events.ToListAsync();
            var events = query.GroupBy(x => x.Day)
                              .Select(g => new { g.Key, Events = g.OrderBy(x => x.Datetime).ToList() })
                              .OrderBy(x => x.Key)
                              .ToList();
            foreach (var group in events)
            {    
                EventForScheduleListViewDto eventForSchedule = new EventForScheduleListViewDto();
                var eventList = Map(group.Events);
                eventForSchedule.Day = group.Key; 
                eventForSchedule.Events = eventList;
                eventForScheduleList.Add(eventForSchedule);
            }
            return eventForScheduleList;
        }

        private List<EventForListViewDto> Map(List<Event> events)
        {
            List<EventForListViewDto> eventList = new List<EventForListViewDto>();
            foreach (var e in events)
            {

                var newEvent = _mapper.Map<EventForListViewDto>(e);
                eventList.Add(newEvent);
            }
            return eventList;
        }
    }
}