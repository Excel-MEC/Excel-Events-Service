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
        Task<EventForDetailedViewDto> GetEvent(int id, int? excelId);  
        Task<Event> GetEventWithTeam(int eventId, int teamId);  
        Task<List<EventWithResultDto>> GetEventsWithResults(); 
        Task<Event> AddEvent(DataForAddingEventDto newEvent);
        Task<Event> UpdateEvent(DataForUpdatingEventDto newEvent);
        Task<Event> DeleteEvent(DataForDeletingEventDto dataForDeletingEvent);
    }
}