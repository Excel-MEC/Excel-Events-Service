using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos.Teams;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class TeamRepository : ITeamRepository
    {
        private readonly DataContext _context;

        public TeamRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Team>> AllTeams()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team> CreateTeam(DataForAddingTeamDto dataForAddingTeam)
        {
            var newTeam = new Team() {Name = dataForAddingTeam.Name, EventId = dataForAddingTeam.EventId};
            _context.Teams.Add(newTeam);
            await _context.SaveChangesAsync();
            return newTeam;
        }

        public async Task<Team> FindTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            return team;
        }

        public async Task<List<Team>> FindEventTeams(int eventId)
        {
            System.Console.WriteLine(eventId);
            return await _context.Teams.Where(team => team.EventId == eventId).ToListAsync();
        }
    }
}