using System.Collections.Generic;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Event;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace API.Data
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public RegistrationRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> ClearUserData(int excelId)
        {
            List<Registration> registeredEventList = await _context.Registrations.Where(r => r.ExcelId == excelId).ToListAsync();
            _context.RemoveRange(registeredEventList);
            var success = await _context.SaveChangesAsync() > 0;
            return success;
        }

        public async Task<List<EventForListViewDto>> EventList(int excelId)
        {
            List<Registration> registrations = await _context.Registrations.Where(r => r.ExcelId == excelId).Include(x => x.Event).ToListAsync();
            List<EventForListViewDto> eventList = new List<EventForListViewDto>();
            foreach (var x in registrations)
            {
                var eventForView = _mapper.Map<EventForListViewDto>(x.Event);
                eventList.Add(eventForView);
            }
            return eventList;
        }

        // marks the bookmarked event as registered upon registration of that event.
        private async Task<bool> HasBookmarked(int excelId, int eventId)
        {
            var fav = await _context.Bookmarks.FirstOrDefaultAsync(x => x.ExcelId == excelId && x.EventId == eventId);
            if (fav != null)
            {
                fav.IsRegistered = true;
            }
            return false;
        }

        public async Task<bool> HasRegistered(int excelId, int eventId)
        {
            var success = await _context.Registrations.FirstOrDefaultAsync(x => x.ExcelId == excelId && x.EventId == eventId);
            if (success != null) return true;
            return false;
        }

        public async Task<bool> Register(int excelId, int eventId)
        {
            Registration user = new Registration();
            user.EventId = eventId;
            user.ExcelId = excelId;
            _context.Registrations.Add(user);
            bool success = await _context.SaveChangesAsync() > 0;
            var hasChanged = await HasBookmarked(excelId, eventId);
            return success;
        }

        public async Task<List<int>> UserList(int eventId)
        {
            var e = await _context.Events.Include(x => x.Registrations).FirstOrDefaultAsync(x => x.Id == eventId);
            List<int> userList = new List<int>();
            foreach (var x in e.Registrations)
            {
                userList.Add(x.ExcelId);
            }
            return userList;
        }
    }
}