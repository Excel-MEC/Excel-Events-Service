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
        Task<RegistrationForViewDto> Register(int excelId, DataForRegistrationDto dataForRegistration);
        Task<List<RegistrationForViewDto>> ClearUserData(DataForClearingUserRegistrationDto dataForClearingUserRegistration);
        Task<List<EventForListViewDto>> EventList(int excelId);
        Task<List<RegistrationWithUserViewDto>> UserList(int eventId);
        Task<bool> HasRegistered(int excelId, int eventId);
        Task<RegistrationForViewDto> RemoveRegistration(int excelId, int eventId);
        Task<RegistrationForViewDto> ChangeTeam(int excelId, DataForRegistrationDto dataForRegistration);
    }
}