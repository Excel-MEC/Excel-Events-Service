using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Registration;
using API.Dtos.Teams;
using API.Extensions.CustomExceptions;
using API.Models;
using API.Services.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public TeamRepository(DataContext context, IMapper mapper, IAccountService accountService)
        {
            _context = context;
            _mapper = mapper;
            _accountService = accountService;
        }

        public async Task<List<Team>> AllTeams()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team> CreateTeam(DataForAddingTeamDto dataForAddingTeam)
        {
            try
            {
                var newTeam = new Team() {Name = dataForAddingTeam.Name, EventId = dataForAddingTeam.EventId};
                _context.Teams.Add(newTeam);
                await _context.SaveChangesAsync();
                return newTeam;
            }
            catch (DbUpdateException)
            {
                throw new DataInvalidException("Team name already taken");
            }
        }

        public async Task<TeamForViewDto> FindTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            var registrations = await _context.Registrations.Where(registration =>
                registration.EventId == team.EventId && registration.TeamId == team.Id).ToListAsync();
            var excelIds = registrations.Select(registration => registration.ExcelId).ToArray();
            var members = new List<UserForViewDto>();
            if (excelIds.Length > 0)
                members = await _accountService.GetUsers(excelIds);
            var teamForView = _mapper.Map<TeamForViewDto>(team);
            teamForView.Members = members;
            return teamForView;
        }

        public async Task<List<Team>> FindEventTeams(int eventId)
        {
            System.Console.WriteLine(eventId);
            return await _context.Teams.Where(team => team.EventId == eventId).ToListAsync();
        }
    }
}