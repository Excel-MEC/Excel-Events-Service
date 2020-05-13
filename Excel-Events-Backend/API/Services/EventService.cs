using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace API.Services
{
    public class EventService : IEventService
    {
        private readonly ICloudStorage _cloudStorage;

        public EventService(ICloudStorage cloudStorage)
        {
            _cloudStorage = cloudStorage;
        }

        public async Task<string> UploadEventIcon(string name, IFormFile icon)
        {
            string fileNameForStorage = GetFilenameForStorage(name, icon.FileName);
            await _cloudStorage.UploadFileAsync(icon, fileNameForStorage);
            string imageUrl = Environment.GetEnvironmentVariable("CLOUD_STORAGE_URL") + fileNameForStorage;
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