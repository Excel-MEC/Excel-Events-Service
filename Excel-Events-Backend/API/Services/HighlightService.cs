using System;
using System.IO;
using System.Threading.Tasks;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace API.Services
{
    public class HighlightService : IHighlightService
    {
        private readonly ICloudStorage _cloudStorage;

        public HighlightService(ICloudStorage cloudStorage)
        {
            _cloudStorage = cloudStorage;
        }

        public async Task<string> UploadHighlightImage(string name, IFormFile icon)
        {
            string fileNameForStorage = GetFilenameForStorage(name, icon.FileName);
            await _cloudStorage.UploadFileAsync(icon, fileNameForStorage);
            string imageUrl = Environment.GetEnvironmentVariable("CLOUD_STORAGE_URL") + fileNameForStorage;
            return imageUrl;
        }
        public async Task DeleteHighlightImage(int id, string filename)
        {
            await _cloudStorage.DeleteFileAsync(GetFilenameForStorage(id.ToString(), filename));
        }
        private string GetFilenameForStorage(string name, string filename)
        {
            return "events/highlights/" + name + Path.GetExtension(filename);
        }
    }
}