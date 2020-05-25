using System.Collections.Generic;
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
            List<Registration> registeredEventList = await _context.Registrations.ToListAsync();
            foreach (var e in registeredEventList)
            {
                if (e.ExcelId == excelId)
                {
                    _context.Remove(e);
                }
            }
            var success = await _context.SaveChangesAsync() > 0;
            return success;
        }

        public async Task<List<EventForListViewDto>> EventList(int excelId)
        {
            List<Registration> registeredEventList = await _context.Registrations.Include(x => x.Event).ToListAsync();
            List<EventForListViewDto> eventList = new List<EventForListViewDto>();
            foreach (var x in registeredEventList)
            {
                if (x.ExcelId == excelId)
                {
                    var eventForView = _mapper.Map<EventForListViewDto>(x.Event);
                    eventList.Add(eventForView);
                }
            }
            return eventList;
        }

        public async Task<bool> HasRegistered(int excelId, int eventId)
        {
            List<Registration> registeredEventList = await _context.Registrations.Include(x => x.Event).ToListAsync();
            var success = registeredEventList.Find(r => r.ExcelId==excelId && r.EventId == eventId);
            if(success!=null) return true;
            return false;
        }

        public async Task<bool> Register(int excelId, int eventId)
        {
            Registration user = new Registration();
            user.EventId = eventId;
            user.ExcelId = excelId;
            _context.Registrations.Add(user);
            bool success = await _context.SaveChangesAsync() > 0;
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