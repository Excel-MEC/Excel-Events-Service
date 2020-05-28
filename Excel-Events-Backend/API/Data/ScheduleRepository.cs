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
            var events = _context.Events
                            .ToLookup(
                                x => x.Day
                                )
                            .OrderBy(x => x.Key)
                            .ToList();

            // .FromSqlRaw("SELECT \"Day\" as \"Id\",json_agg(events ORDER By \"Datetime\") as Events FROM \"public\".\"Events\" as events GROUP BY \"Day\"")


            // foreach (var group in events)
            // {    
            //     EventForScheduleListViewDto eventForSchedule = new EventForScheduleListViewDto();
            //     eventForSchedule.Day = group.Key;
            //     List<EventForListViewDto> eventList = new List<EventForListViewDto>();
            //     foreach (var e in group)
            //     {

            //         var newEvent = _mapper.Map<EventForListViewDto>(e);
            //         eventList.Add(newEvent);
            //     }
            //     eventForSchedule.Events = eventList;
            //     eventForScheduleList.Add(eventForSchedule);
            // }
            // // return Map(events);
            return eventForScheduleList;
        }

        // private List<EventForListViewDto> Map(List<Event> events)
        // {
        //     List<EventForListViewDto> eventList = new List<EventForListViewDto>();
        //     foreach (var e in events)
        //     {

        //         var newEvent = _mapper.Map<EventForListViewDto>(e);
        //         eventList.Add(newEvent);
        //     }
        //     return eventList;
        // }
    }
}