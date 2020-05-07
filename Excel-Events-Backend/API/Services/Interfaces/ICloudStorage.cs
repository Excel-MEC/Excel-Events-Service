using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API.Services.Interfaces
{
    public interface ICloudStorage
    {
        // Uploads the file given with the parameter imageFile to the CS with the given fileNameForStorage as object name.
        Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage);

        // Deletes the file with the given fileNameForStorage as object name from the CS bucket.
        Task DeleteFileAsync(string fileNameForStorage);
    }
}