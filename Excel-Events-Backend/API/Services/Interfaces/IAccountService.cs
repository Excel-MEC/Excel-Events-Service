using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos.Registration;

namespace API.Services.Interfaces
{
    public interface IAccountService
    {
        Task<List<UserForViewDto>> GetUsers(int[] excelIds);
    }
}