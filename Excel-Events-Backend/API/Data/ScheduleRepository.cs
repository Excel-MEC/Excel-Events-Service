using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using API.Data.Interfaces;
using API.Dtos.Schedule;
using API.Extensions.CustomExceptions;
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
                .Select(r => _mapper.Map<EventRoundForScheduleViewDto>(r))
                .ToListAsync();
            var events = eventList.GroupBy(x => x.Day)
                .Select(g => new
                {
                    g.Key,
                    Events = g.OrderBy(x => x.Datetime).ToList()
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
                    Venue = e.Event.Venue,
                    Round = e.Round,
                    Datetime = e.Datetime,
                    Day = e.Day,
                    NeedRegistration = e.Event.NeedRegistration
                })
                .ToList();
        }

        public async Task<ScheduleForViewDto> AddSchedule(DataForScheduleDto dataFromClient)
        {
            var eventFromDb = await _context.Events.Include(e => e.Rounds)
                .FirstOrDefaultAsync(e => e.Id == dataFromClient.EventId);
            if (eventFromDb == null) throw new DataInvalidException("Invalid event ID");
            var newRound = _mapper.Map<Schedule>(dataFromClient);
            eventFromDb.Rounds.Add(newRound);
            eventFromDb.NumberOfRounds += 1;
            if (dataFromClient.RoundId == 0)
            {
                eventFromDb.Day = dataFromClient.Day;
                eventFromDb.Datetime = dataFromClient.Datetime;
            }

            await _context.Rounds.AddAsync(newRound);
            await _context.SaveChangesAsync();
            return _mapper.Map<ScheduleForViewDto>(newRound);
        }

        public async Task<ScheduleForViewDto> UpdateSchedule(DataForScheduleDto dataFromClient)
        {
            var eventFromSchedule = await _context.Rounds.FirstOrDefaultAsync(e =>
                e.EventId == dataFromClient.EventId && e.RoundId == dataFromClient.RoundId);
            eventFromSchedule.Day = dataFromClient.Day;
            eventFromSchedule.Datetime = dataFromClient.Datetime;
            eventFromSchedule.Round = dataFromClient.Round;
            if (dataFromClient.RoundId == 0)
            {
                var scheduledEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == dataFromClient.EventId);
                scheduledEvent.Day = dataFromClient.Day;
                scheduledEvent.Datetime = dataFromClient.Datetime;                
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<ScheduleForViewDto>(eventFromSchedule);
        }

        public async Task<ScheduleForViewDto> RemoveSchedule(DataForDeletingScheduleDto dataFromClient)
        {
            var eventFromSchedule = await _context.Rounds.FirstOrDefaultAsync(e =>
                e.EventId == dataFromClient.EventId && e.RoundId == dataFromClient.RoundId);
            _context.Remove(eventFromSchedule);
            if (dataFromClient.RoundId == 0)
            {
                var scheduledEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == dataFromClient.EventId);
                scheduledEvent.Day = default(int);
                scheduledEvent.Datetime = default(DateTime);
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<ScheduleForViewDto>(eventFromSchedule);
        }
    }
}