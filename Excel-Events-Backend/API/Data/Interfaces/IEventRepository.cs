using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.Event;

namespace API.Data.Interfaces
{
    public interface IEventRepository
    {
        Task<List<EventForListViewDto>> EventList();
        Task<List<EventForListViewDto>> FilteredList(int eventType, int category);
        Task<List<EventForListViewDto>> EventListOfType(int id);
        Task<List<EventForListViewDto>> EventListOfCategory(int id);
        Task<EventForDetailedViewDto> GetEvent(int id);
        Task<bool> AddRound(DataForAddingEventRoundDto newRound);   
        Task<bool> AddEvent(DataForAddingEventDto newEvent);
        Task<bool> UpdateEvent(DataForUpdatingEventDto newEvent);
        Task<bool> DeleteEvent(DataForDeletingEventDto dataForDeletingEvent);
    }
}