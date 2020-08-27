using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.EventHeads;
using API.Models;


namespace API.Data.Interfaces
{
    public interface IEventHeadRepository
    {
        Task<List<EventHeadForViewDto>> ListEventHeads();  
        Task<EventHeadForViewDto> GetEventHead(int id);
        Task<EventHead> AddEventHead(DataForAddingEventHead newEventHead);
        Task<EventHead> UpdateEventHead(DataForUpdatingEventHeadDto newEventHead);
        Task<EventHead> DeleteEventHead(DataForDeletingEventHeadDto dataForDeletingEventHead);
    }
}