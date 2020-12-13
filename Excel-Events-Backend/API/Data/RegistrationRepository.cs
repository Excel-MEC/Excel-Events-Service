using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Event;
using API.Dtos.Registration;
using API.Extensions.CustomExceptions;
using API.Models;
using API.Models.Custom;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IEventRepository _eventRepo;
        private readonly IEnvironmentService _env;

        public RegistrationRepository(DataContext context, IMapper mapper, IEventRepository eventRepo,
            IEnvironmentService env)
        {
            _mapper = mapper;
            _context = context;
            _eventRepo = eventRepo;
            _env = env;
        }


        public async Task<RegistrationForViewDto> Register(int excelId, DataForRegistrationDto dataForRegistration)
        {
            if (await HasRegistered(excelId, dataForRegistration.EventId))
                throw new OperationInvalidException("Already registered for the event.");
            if (dataForRegistration.TeamId != null)
                return await RegisterWithTeam(excelId, dataForRegistration.EventId,
                    Convert.ToInt32(dataForRegistration.TeamId));
            var eventToRegister = await _eventRepo.GetEvent(dataForRegistration.EventId, null);
            if (eventToRegister == null) throw new DataInvalidException("Invalid event ID.");
            if (eventToRegister.IsTeam) throw new DataInvalidException("Need team Id to register for team event.");
            var newRegistration = new Registration {EventId = dataForRegistration.EventId, ExcelId = excelId};
            await _context.Registrations.AddAsync(newRegistration);
            await _context.SaveChangesAsync();
            return _mapper.Map<RegistrationForViewDto>(newRegistration);
        }


        public async Task<List<RegistrationForViewDto>> ClearUserData(
            DataForClearingUserRegistrationDto dataForClearingUserRegistration)
        {
            var registeredEventList = await _context.Registrations
                .Where(r => r.ExcelId == dataForClearingUserRegistration.ExcelId).ToListAsync();
            if (registeredEventList.Count == 0)
                throw new DataInvalidException("Invalid excel ID. Please re-check the excel ID");
            _context.RemoveRange(registeredEventList);
            await _context.SaveChangesAsync();
            return registeredEventList.Select(x => _mapper.Map<RegistrationForViewDto>(x)).ToList();
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
            var success =
                await _context.Registrations.FirstOrDefaultAsync(x => x.ExcelId == excelId && x.EventId == eventId);
            return success != null;
        }

        public async Task<RegistrationForViewDto> RemoveRegistration(int excelId, int eventId)
        {
            var registration =
                await _context.Registrations.FirstOrDefaultAsync(x => x.ExcelId == excelId && x.EventId == eventId);
            if (registration == null) throw new DataInvalidException("Invalid excel ID or event ID");
            _context.Remove(registration);
            await _context.SaveChangesAsync();
            return _mapper.Map<RegistrationForViewDto>(registration);
        }

        public async Task<RegistrationForViewDto> ChangeTeam(int excelId, DataForRegistrationDto dataForRegistration)
        {
            var eventWithTeams = await _eventRepo.GetEventWithTeam(dataForRegistration.EventId,
                Convert.ToInt32(dataForRegistration.TeamId));
            if(eventWithTeams.EventStatusId != 1) throw new DataInvalidException("Event has started");
            if (eventWithTeams.Registrations.Count < eventWithTeams.TeamSize)
            {
                var registration = await _context.Registrations.FirstOrDefaultAsync(r =>
                    r.EventId == dataForRegistration.EventId && r.ExcelId == excelId);
                registration.TeamId = dataForRegistration.TeamId;
                await _context.SaveChangesAsync();
                registration.Team = await _context.Teams.AsNoTracking()
                    .FirstOrDefaultAsync(team => team.Id == dataForRegistration.TeamId);
                return _mapper.Map<RegistrationForViewDto>(registration);
            }

            throw new DataInvalidException("Team is full");
        }

        public async Task<List<UserForViewDto>> UserList(int eventId)
        {
            var registrations = await _context.Registrations.Where(x => x.EventId == eventId).ToListAsync();
            var ids = registrations.Select(x => x.ExcelId).ToArray();
            var users = new List<UserForViewDto>();
            if (ids.Length > 0)
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("ServiceAuthorization",
                        _env.ServiceKey);
                    var response = await client.PostAsync(
                        $"{_env.AccountsHost}/api/admin/users",
                        new StringContent(JsonSerializer.Serialize(ids), Encoding.UTF8, "application/json"));
                    var responseString = await response.Content.ReadAsStringAsync();
                    users = JsonSerializer.Deserialize<List<UserForViewDto>>(responseString);
                }

            return users;
        }

        private async Task<RegistrationForViewDto> RegisterWithTeam(int excelId, int eventId, int teamId)
        {
            var eventToRegister = await _eventRepo.GetEventWithTeam(eventId, teamId);
            if (eventToRegister == null) throw new DataInvalidException("Invalid event ID.");
            if (!eventToRegister.IsTeam) throw new DataInvalidException("Given event is not team event");
            if (eventToRegister.TeamSize < eventToRegister.Registrations.Count)
                throw new DataInvalidException("Team is full");
            var newRegistration = new Registration {EventId = eventId, ExcelId = excelId, TeamId = teamId};
            await _context.Registrations.AddAsync(newRegistration);
            await _context.SaveChangesAsync();
            newRegistration.Team = await _context.Teams.AsNoTracking().FirstOrDefaultAsync(team => team.Id == teamId);
            return _mapper.Map<RegistrationForViewDto>(newRegistration);
        }
    }
}