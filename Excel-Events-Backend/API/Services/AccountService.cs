using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using API.Dtos.Registration;
using System.Text.Json;
using API.Services.Interfaces;

namespace API.Services
{
    public class AccountService : IAccountService
    {
        private readonly IEnvironmentService _env;
        public AccountService(IEnvironmentService env)
        {
            _env = env;
        }
        public async Task<List<UserForViewDto>> GetUsers(int[] excelIds)
        {
            var users = new List<UserForViewDto>();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("ServiceAuthorization",
                    _env.ServiceKey);
                var response = await client.PostAsync(
                    $"{_env.AccountsHost}/api/admin/users",
                    new StringContent(JsonSerializer.Serialize(excelIds), Encoding.UTF8, "application/json"));
                var responseString = await response.Content.ReadAsStringAsync();
                users = JsonSerializer.Deserialize<List<UserForViewDto>>(responseString);
            }

            return users;
        }
    }
}