using System;
using System.IO;
using System.Threading.Tasks;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace API.Services
{
    public class EventService : IEventService
    {
        private readonly ICloudStorage _cloudStorage;
        private readonly IEnvironmentService _env;

        public EventService(ICloudStorage cloudStorage, IEnvironmentService env)
        {
            _cloudStorage = cloudStorage;
            _env = env;
        }

        public async Task<string> UploadEventIcon(string name, IFormFile icon)
        {
            string fileNameForStorage = GetFilenameForStorage(name, icon.FileName);
            await _cloudStorage.UploadFileAsync(icon, fileNameForStorage);
            string imageUrl = _env.CloudStorageUrl + fileNameForStorage;
            return imageUrl;
        }
        public async Task DeleteEventIcon(int id, string filename)
        {
            await _cloudStorage.DeleteFileAsync(GetFilenameForStorage(id.ToString(), filename));
        }
        private string GetFilenameForStorage(string name, string filename)
        {
            return "events/icons/" + name + Path.GetExtension(filename);
        }
    }
}