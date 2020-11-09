using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.Teams;
using API.Models;

namespace API.Data.Interfaces
{
    public interface ITeamRepository
    {
        Task<List<Team>> AllTeams();
        Task<Team> CreateTeam(DataForAddingTeamDto dataForAddingTeam);
        Task<Team> FindTeam(int id);
        Task<List<Team>> FindEventTeams(int eventId);
    }
}