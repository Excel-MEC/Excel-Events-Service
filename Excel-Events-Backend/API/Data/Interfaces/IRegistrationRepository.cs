using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Dtos.Event;
using API.Dtos.Registration;
using API.Models;

namespace API.Data.Interfaces
{
    public interface IRegistrationRepository
    {
        Task<RegistrationForViewDto> Register(int excelId, int eventId);
        Task<List<RegistrationForViewDto>> ClearUserData(int excelId);
        Task<List<EventForListViewDto>> EventList(int excelId);
        Task<List<UserForViewDto>> UserList(int eventId);
        Task<bool> HasRegistered(int excelId, int eventId);
        Task<RegistrationForViewDto> RemoveRegistration(int excelId, int eventId);
    }
}