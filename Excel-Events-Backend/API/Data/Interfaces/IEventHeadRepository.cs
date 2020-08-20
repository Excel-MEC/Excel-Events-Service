using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.EventHeads;


namespace API.Data.Interfaces
{
    public interface IEventHeadRepository
    {
        Task<List<EventHeadForViewDto>> ListEventHeads();  
        Task<EventHeadForViewDto> GetEventHead(int id);
        Task<bool> AddEventHead(DataForAddingEventHead newEventHead);
        Task<bool> UpdateEventHead(DataForUpdatingEventHeadDto newEventHead);
        Task<bool> DeleteEventHead(DataForDeletingEventHeadDto dataForDeletingEventHead);
    }
}