using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Models;

namespace API.Data.Interfaces
{
    public interface IResultRepository
    {
        Task<ResultForListViewDto> GetEventResults(int eventId);
        Task<Result> AddEventResult(DataForAddingResultDto dataFromClient);
        Task<Result> UpdateEventResult(DataForUpdatingResultDto dataFromClient);
        Task<Result> RemoveResult(int resultId);
        Task<List<Result>> RemoveAllResults(int eventId);
    }
}