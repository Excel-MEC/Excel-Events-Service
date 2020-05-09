using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Services.Interfaces
{
    public interface IHighlightService
    {
        Task<string> UploadHighlightImage(string name, IFormFile icon);
        Task DeleteHighlightImage(int id, string filename);
    }
}