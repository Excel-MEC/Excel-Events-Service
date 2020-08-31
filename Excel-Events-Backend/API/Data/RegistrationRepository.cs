using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Event;
using API.Dtos.Registration;
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

        public async Task<List<RegistrationForViewDto>> ClearUserData(int excelId)
        {
            var registeredEventList = await _context.Registrations.Where(r => r.ExcelId == excelId).ToListAsync();
            if(registeredEventList.Count == 0) throw new DataInvalidException("Invalid excel ID. Please re-check the excel ID");
            _context.RemoveRange(registeredEventList);
            await _context.SaveChangesAsync();
            return registeredEventList.Select(x=>_mapper.Map<RegistrationForViewDto>(x)).ToList();
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

        public async Task<RegistrationForViewDto> RemoveRegistration(int excelId, int eventId)
        {
            var registration = await _context.Registrations.FirstOrDefaultAsync(x => x.ExcelId == excelId && x.EventId == eventId);
            if(registration == null) throw new DataInvalidException("Invalid excel ID or event ID");
            _context.Remove(registration);
            await _context.SaveChangesAsync();
            return _mapper.Map<RegistrationForViewDto>(registration);
        }

        public async Task<RegistrationForViewDto> Register(int excelId, int eventId)
        {
            if(await HasRegistered(excelId,eventId)) throw new OperationInvalidException("Already registered for the event.");
            var eventToRegister = await _context.Events.FirstOrDefaultAsync(x => x.Id == eventId);
            if (eventToRegister == null) throw new DataInvalidException("Invalid event ID.");
            var newRegistration = new Registration {EventId = eventId, ExcelId = excelId};
            await _context.Registrations.AddAsync(newRegistration);
            await _context.SaveChangesAsync() ;
            return _mapper.Map<RegistrationForViewDto>(newRegistration);
        }

        public async Task<List<int>> UserList(int eventId)
        {
            var registrations = await _context.Registrations.Where(x => x.EventId == eventId).ToListAsync();
            return registrations.Select(x => x.ExcelId).ToList();
        }
    }
}