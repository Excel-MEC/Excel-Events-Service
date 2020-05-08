using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.Event;
using API.Models;

namespace API.Data.Interfaces
{
    public interface IEventRepository
    {
        Task<List<EventForListViewDto>> EventList();
        Task<List<EventForListViewDto>> FilteredList(int eventType, int category);
        Task<List<EventForListViewDto>> EventListOfType(int id);
        Task<List<EventForListViewDto>> EventListOfCategory(int id);
        Task<Event> GetEvent(int id);
        Task<bool> AddEvent(DataForAddingEventDto newEvent);
        Task<bool> UpdateEvent(DataForUpdatingEventDto newEvent);
    }
}