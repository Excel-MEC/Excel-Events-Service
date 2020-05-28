using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.Schedule;
using API.Models;

namespace API.Data.Interfaces
{
    public interface IScheduleRepository
    {
        Task<List<EventForScheduleListViewDto>> EventList();    
    }
}