using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.Schedule;

namespace API.Data.Interfaces
{
    public interface IScheduleRepository
    {    
        Task<List<EventForScheduleListViewDto>> ScheduleList();
        Task<bool> AddSchedule(DataForScheduleDto dataFromClient); 
        Task<bool> UpdateSchedule(DataForScheduleDto dataFromClient);
        Task<bool> RemoveSchedule(DataForDeletingScheduleDto dataFromClient);
    }
}