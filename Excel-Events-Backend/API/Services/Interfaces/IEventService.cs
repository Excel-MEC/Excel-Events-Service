using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Services.Interfaces
{
    public interface IEventService
    {
        Task DeleteEventIcon(int id, string filename);
        Task<string> UploadEventIcon(string name, IFormFile icon);
    }
}