using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.Schedule;
using API.Models;

namespace API.Data.Interfaces
{
    public interface IScheduleRepository
    {    
        Task<List<EventForScheduleListViewDto>> ScheduleList();
        Task<ScheduleViewDto> AddSchedule(DataForScheduleDto dataFromClient); 
        Task<ScheduleViewDto> UpdateSchedule(DataForScheduleDto dataFromClient);
        Task<ScheduleViewDto> RemoveSchedule(DataForDeletingScheduleDto dataFromClient);
    }
}