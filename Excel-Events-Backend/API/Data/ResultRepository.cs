using System;
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
                var newResult = _mapper.Map<Result>(dataFromClient);
                _context.Results.Add(newResult);
                await _context.SaveChangesAsync();
                return newResult;
            }
            catch (DbUpdateException)
            {
                throw new DataInvalidException("Invalid Data");
            }
        }

        public async Task<ResultForListViewDto> GetEventResults(int eventId)
        {
            var responseFromDb = await _context.Results.Where(r => r.EventId == eventId)
                                                        .Select(r => _mapper.Map<ResultForViewDto>(r))
                                                        .ToListAsync();
            if (responseFromDb == null) throw new DataInvalidException("Invalid event ID");
            var isTeam = responseFromDb[0].TeamId > 0;
            var results = responseFromDb.OrderByDescending(r => r.Position).ToList();
            var resultsForView = new ResultForListViewDto() { isTeam = isTeam, Results = results };
            return resultsForView;
        }

        public async Task<List<Result>> RemoveAllResults(int eventId)
        {
            try
            {
                var resultFromDb = _context.Results.Where(r => r.EventId == eventId).ToList();
                if (resultFromDb == null) throw new DataInvalidException("Invalid event ID");
                _context.RemoveRange(resultFromDb);
                await _context.SaveChangesAsync();
                return resultFromDb;
            }
            catch (DbUpdateException)
            {
                throw new DataInvalidException("Problem saving data. Try again");
            }
        }

        public async Task<Result> RemoveResult(int resultId)
        {
            try
            {
                var resultFromDb = await _context.Results.FirstOrDefaultAsync(r => r.Id == resultId);
                _context.Remove(resultFromDb);
                await _context.SaveChangesAsync();
                return resultFromDb;
            }
            catch (DbUpdateException)
            {
                throw new DataInvalidException("Problem saving data. Try again");
            }
            catch (ArgumentNullException)
            {
                throw new DataInvalidException("Invalid Result Id. Try again");
            }
        }

        public async Task<Result> UpdateEventResult(DataForUpdatingResultDto dataFromClient)
        {
            try
            {
                var resultFromDb = await _context.Results.FindAsync(dataFromClient.Id);
                resultFromDb.Name = dataFromClient.Name;
                resultFromDb.EventId = dataFromClient.EventId;
                resultFromDb.ExcelId = dataFromClient.ExcelId;
                resultFromDb.TeamId = dataFromClient.TeamId;
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