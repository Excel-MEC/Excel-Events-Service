using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.Schedule;

namespace API.Data.Interfaces
{
    public interface IScheduleRepository
    {    
        Task<List<EventForScheduleListViewDto>> ScheduleList();
        Task<ScheduleForViewDto> AddSchedule(DataForScheduleDto dataFromClient); 
        Task<ScheduleForViewDto> UpdateSchedule(DataForScheduleDto dataFromClient);
        Task<ScheduleForViewDto> RemoveSchedule(DataForDeletingScheduleDto dataFromClient);
    }
}