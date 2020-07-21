using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Event;
using API.Models;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class EventRepository : IEventRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IEventService _service;
        public EventRepository(DataContext context, IMapper mapper, IEventService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }

        public async Task<List<EventForListViewDto>> EventList()
        {
            List<EventForListViewDto> events = await _context.Events.Select(e => _mapper.Map<EventForListViewDto>(e)).ToListAsync();
            return events;
        }

        public async Task<List<EventForListViewDto>> FilteredList(int eventTypeId, int categoryId)
        {
            List<Event> filteredEvents = await _context.Events.Where(e => e.EventTypeId == eventTypeId && e.CategoryId == categoryId).ToListAsync();
            List<EventForListViewDto> events = new List<EventForListViewDto>();
            foreach (var e in filteredEvents)
            {
                events.Add(_mapper.Map<EventForListViewDto>(e));
            }
            return events;
        }

        public async Task<List<EventForListViewDto>> EventListOfType(int eventTypeId)
        {
            List<Event> filteredEvents = await _context.Events.Where(e => e.EventTypeId == eventTypeId).ToListAsync();
            List<EventForListViewDto> events = new List<EventForListViewDto>();
            foreach (var e in filteredEvents)
            {
                events.Add(_mapper.Map<EventForListViewDto>(e));
            }
            return events;
        }
        public async Task<List<EventForListViewDto>> EventListOfCategory(int categoryId)
        {
            List<Event> filteredEvents = await _context.Events.Where(e => e.CategoryId == categoryId).ToListAsync();
            List<EventForListViewDto> events = new List<EventForListViewDto>();
            foreach (var e in filteredEvents)
            {
                events.Add(_mapper.Map<EventForListViewDto>(e));
            }
            return events;
        }

        public async Task<Event> GetEvent(int id)
        {
            Event eventFromdb = await _context.Events.Include(e => e.EventHead1).Include(e => e.EventHead2).FirstOrDefaultAsync(e => e.Id == id);
            return eventFromdb;
        }

        public async Task<bool> AddEvent(DataForAddingEventDto eventDataFromClient)
        {
            Event newEvent = _mapper.Map<Event>(eventDataFromClient);
            await _context.Events.AddAsync(newEvent);
            if (await _context.SaveChangesAsync() > 0)
            {
                string imageUrl = await _service.UploadEventIcon(newEvent.Id.ToString(), eventDataFromClient.Icon);
                newEvent.Icon = imageUrl;
                if (await _context.SaveChangesAsync() > 0)
                    return true;
                throw new Exception("Trouble saveing Image Name");
            }
            throw new Exception("Problem Saving Changes");
        }
        public async Task<bool> DeleteEvent(DataForDeletingEventDto dataForDeletingEvent)
        {
            Event eventToDelete = await _context.Events.FindAsync(dataForDeletingEvent.Id);
            if (eventToDelete.Name == dataForDeletingEvent.Name)
            {
                await _service.DeleteEventIcon(dataForDeletingEvent.Id, eventToDelete.Icon);
                _context.Events.Remove(await _context.Events.FindAsync(dataForDeletingEvent.Id));
                bool success = await _context.SaveChangesAsync() > 0;
                return success;
            }
            throw new Exception("Id and Name doesnot match");
        }

        public async Task<bool> UpdateEvent(DataForUpdatingEventDto eventDataFromClient)
        {
            Event eventFromDb = await _context.Events.FindAsync(eventDataFromClient.Id);
            Event eventForUpdate = _mapper.Map<Event>(eventDataFromClient);
            if (eventDataFromClient.Icon != null)
            {
                await _service.DeleteEventIcon(eventFromDb.Id, eventFromDb.Icon);
                string imageUrl = await _service.UploadEventIcon(eventDataFromClient.Id.ToString(), eventDataFromClient.Icon);
                if (!eventFromDb.Icon.Equals(imageUrl))
                    eventForUpdate.Icon = imageUrl;
                else
                    eventForUpdate.Icon = eventFromDb.Icon;
            }
            else
                eventForUpdate.Icon = eventFromDb.Icon;

            CopyChanges(eventForUpdate, eventFromDb);
            _context.SaveChanges();
            return true;
        }
        private void CopyChanges(Event src, Event dest)
        {
            dest.Icon = src.Icon;
            dest.CategoryId = src.CategoryId;
            dest.EventTypeId = src.EventTypeId;
            dest.About = src.About;
            dest.Format = src.Format;
            dest.Rules = src.Rules;
            dest.Venue = src.Venue;
            dest.Datetime = src.Datetime;
            dest.EntryFee = src.EntryFee;
            dest.PrizeMoney = src.PrizeMoney;
            dest.EventHead1Id = src.EventHead1Id;
            dest.EventHead2Id = src.EventHead2Id;
            dest.IsTeam = src.IsTeam;
            dest.TeamSize = src.TeamSize;
            dest.EventStatusId = src.EventStatusId;
            dest.NumberOfRounds = src.NumberOfRounds;
            dest.CurrentRound = src.CurrentRound;
            dest.NeedRegistration = src.NeedRegistration;
        }
    }
}