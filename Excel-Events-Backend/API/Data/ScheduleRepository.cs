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
                    Day = e.Day,
                    NeedRegistration = e.Event.NeedRegistration
                })
                .ToList();
        }
        public async Task<ScheduleViewDto> AddSchedule(DataForScheduleDto dataFromClient)
        {
            var eventFromDb = await _context.Events.Include(e => e.Rounds)
                .FirstOrDefaultAsync(e => e.Id == dataFromClient.EventId);
            if(eventFromDb == null) throw new DataInvalidException("Invalid event ID");
            var newRound = _mapper.Map<Schedule>(dataFromClient);
            eventFromDb.Rounds.Add(newRound);
            eventFromDb.NumberOfRounds += 1;
            if (dataFromClient.RoundId == 0)
            {
                eventFromDb.Day = dataFromClient.Day;
                eventFromDb.Datetime = dataFromClient.Datetime;
            }
            await _context.Rounds.AddAsync(newRound);
            if(await _context.SaveChangesAsync() > 0) return _mapper.Map<ScheduleViewDto>(newRound);
            throw new Exception("Problem in adding new round.");
        }

        public async Task<ScheduleViewDto> UpdateSchedule(DataForScheduleDto dataFromClient)
        {
            var eventFromSchedule = await _context.Rounds.FirstOrDefaultAsync(e =>
                e.EventId == dataFromClient.EventId && e.RoundId == dataFromClient.RoundId);
            eventFromSchedule.Day = dataFromClient.Day;
            eventFromSchedule.Datetime = dataFromClient.Datetime;
            if (dataFromClient.RoundId == 0)
            {
                var scheduledEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == dataFromClient.EventId);
                scheduledEvent.Day = dataFromClient.Day;
                scheduledEvent.Datetime = dataFromClient.Datetime;
            }
            if (await _context.SaveChangesAsync() > 0) return _mapper.Map<ScheduleViewDto>(eventFromSchedule);
            throw new Exception("Problem in updating the schedule.");
        }

        public async Task<ScheduleViewDto> RemoveSchedule(DataForDeletingScheduleDto dataFromClient)
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
            if (await _context.SaveChangesAsync() > 0) return _mapper.Map<ScheduleViewDto>(eventFromSchedule);
            throw new Exception("Problem in deleting the schedule.");
        }
    }
}