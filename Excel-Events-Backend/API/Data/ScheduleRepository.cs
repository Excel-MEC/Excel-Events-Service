using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using API.Data.Interfaces;
using API.Dtos.Schedule;
using API.Models;
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
            var eventForScheduleList = new List<EventForScheduleListViewDto>();
            var eventList = await _context.Rounds.Include(r => r.Event)
                .Select(q => _mapper.Map<EventRoundForScheduleViewDto>(q))
                .ToListAsync();
            var events = eventList.GroupBy(x => x.Day)
                .Select(g => new
                {
                    g.Key,
                    Events = g.OrderBy(x => x.Datetime)
                        .ToList()
                })
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
        private static List<EventForScheduleViewDto> Map(List<EventRoundForScheduleViewDto> events)
        {
            return events.Select(e => new EventForScheduleViewDto
                {
                    Id = e.Event.Id,
                    Name = e.Event.Name,
                    Icon = e.Event.Icon,
                    EventType = e.Event.EventType,
                    Category = e.Event.Category,
                    Round = e.Round,
                    Datetime = e.Datetime,
                    Day = e.Day
                })
                .ToList();
        }
        public async Task<bool> AddRound(DataForAddingEventRoundDto dataFromClient)
        {
            var eventFromdb = await _context.Events.Include(e => e.Rounds)
                .FirstOrDefaultAsync(e => e.Id == dataFromClient.EventId);
            var newRound = _mapper.Map<Schedule>(dataFromClient);
            eventFromdb.Rounds.Add(newRound);
            eventFromdb.NumberOfRounds += 1;
            if (eventFromdb.NumberOfRounds == 1)
            {
                eventFromdb.Day = dataFromClient.Day;
                eventFromdb.Datetime = dataFromClient.Datetime;
            }
            await _context.Rounds.AddAsync(newRound);
            if (await _context.SaveChangesAsync() > 0) return true;
            throw new Exception("Trouble saving new round. ");
        }
    }
}