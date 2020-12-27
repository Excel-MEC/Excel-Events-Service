using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Interfaces;
using API.Dtos;
using API.Extensions.CustomExceptions;
using API.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ResultRepository : IResultRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ResultRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> AddEventResult(DataForAddingResultDto dataFromClient)
        {
            try
            {
                var newResult = new Result()
                {
                    EventId = dataFromClient.EventId,
                    Name = dataFromClient.Name,
                    Position = dataFromClient.Position,
                    TeamMembers = dataFromClient.TeamMembers,
                    TeamName = dataFromClient.TeamName
                };

                _context.Results.Add(newResult);
                await _context.SaveChangesAsync();
                return newResult;
            }
            catch (DbUpdateException)
            {
                throw new DataInvalidException("Invalid Data");
            }
        }

        public async Task<List<Result>> AllResults()
        {
            var responseFromDb = await _context.Results.ToListAsync();
            return responseFromDb;
        }

        public async Task<List<ResultForViewDto>> GetEventResults(int eventId)
        {
            var responseFromDb = await _context.Results.Where(r => r.EventId == eventId)
                                                        .Select(r => _mapper.Map<ResultForViewDto>(r))
                                                        .ToListAsync();
            if (responseFromDb == null) throw new DataInvalidException("Invalid event ID");
            var results = responseFromDb.OrderByDescending(r => r.Position).ToList();

            return results;
        }

        public async Task<Result> UpdateEventResult(DataForUpdatingResultDto dataFromClient)
        {
            try
            {
                var resultFromDb = await _context.Results.FindAsync(dataFromClient.Id);
                resultFromDb.Name = dataFromClient.Name;
                resultFromDb.EventId = dataFromClient.EventId;
                resultFromDb.Position = dataFromClient.Position;
                resultFromDb.TeamMembers = dataFromClient.TeamMembers;
                resultFromDb.TeamName = dataFromClient.TeamName;
                await _context.SaveChangesAsync();
                return resultFromDb;
            }
            catch (DbUpdateException)
            {
                throw new DataInvalidException("Problem saving data. Try again");
            }
        }
    }
}