using System;
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

        private async Task<bool> UpdateBookmark(int excelId, int eventId)
        {
            var fav = await _context.Bookmarks.FirstOrDefaultAsync(x => x.ExcelId == excelId && x.EventId == eventId);
            if (fav == null) return false;
            fav.IsRegistered = true;
            if(await _context.SaveChangesAsync() <= 0) throw new Exception("Problem saving changes");
            return true;
        }

        public async Task<bool> HasRegistered(int excelId, int eventId)
        {
            var success = await _context.Registrations.FirstOrDefaultAsync(x => x.ExcelId == excelId && x.EventId == eventId);
            return success != null;
        }

        public async Task<bool> Register(int excelId, int eventId)
        {
            var eventToRegister = await _context.Events.FirstOrDefaultAsync(x => x.Id == eventId);
            if (!(bool) eventToRegister.NeedRegistration) throw new Exception("This event need no registration.");
            var user = new Registration {EventId = eventId, ExcelId = excelId};
            await _context.Registrations.AddAsync(user);
            var success = await _context.SaveChangesAsync() > 0;
            if(success) return await UpdateBookmark(excelId, eventId);
            throw new Exception("Problem saving changes");
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