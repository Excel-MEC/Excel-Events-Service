using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.Event;

namespace API.Data.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<bool> Register(int excelId, int eventId);
        Task<bool> ClearUserData(int excelId);
        Task<List<EventForListViewDto>> EventList(int excelId);
        List<int> UserList(int eventId);
        Task<bool> HasRegistered(int excelId, int eventId);
    }
}