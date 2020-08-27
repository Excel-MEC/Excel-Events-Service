using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Event;
using API.Extensions.CustomExceptions;
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

        public async Task<List<Registration>> ClearUserData(int excelId)
        {
            var registeredEventList = await _context.Registrations.Where(r => r.ExcelId == excelId).ToListAsync();
            if(registeredEventList.Count == 0) throw new DataInvalidException("Invalid excel ID. Please re-check the excel ID");
            _context.RemoveRange(registeredEventList);
            if( await _context.SaveChangesAsync() > 0) return registeredEventList;
            throw new Exception("Problem clearing user data.");
        }

        public async Task<List<EventForListViewDto>> EventList(int excelId)
        {
            var registrations = await _context.Registrations.Where(r => r.ExcelId == excelId)
                .Include(x => x.Event)
                .ToListAsync();
            return registrations.Select(x => _mapper.Map<EventForListViewDto>(x.Event)).ToList();
        }

        public async Task<bool> HasRegistered(int excelId, int eventId)
        {
            var success = await _context.Registrations.FirstOrDefaultAsync(x => x.ExcelId == excelId && x.EventId == eventId);
            return success != null;
        }

        public async Task<Registration> Register(int excelId, int eventId)
        {
            if(await HasRegistered(excelId,eventId)) throw new OperationInvalidException("Already registered for the event.");
            var eventToRegister = await _context.Events.FirstOrDefaultAsync(x => x.Id == eventId);
            if (eventToRegister == null) throw new DataInvalidException("Invalid event ID.");
            var newRegistration = new Registration {EventId = eventId, ExcelId = excelId};
            await _context.Registrations.AddAsync(newRegistration);
            if (await _context.SaveChangesAsync() <= 0) throw new Exception("Problem registering user");
            return newRegistration;
        }

        public async Task<List<int>> UserList(int eventId)
        {
            var registrations = await _context.Registrations.Where(x => x.EventId == eventId).ToListAsync();
            return registrations.Select(x => x.ExcelId).ToList();
        }
    }
}