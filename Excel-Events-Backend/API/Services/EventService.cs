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
        private readonly IConfiguration _configuration;

        public EventService(ICloudStorage cloudStorage, IConfiguration configuration)
        {
            _cloudStorage = cloudStorage;
            _configuration = configuration;
        }

        public async Task<string> UploadEventIcon(string name, IFormFile icon)
        {
            string fileNameForStorage = "events/icons/" + name + Path.GetExtension(icon.FileName);
            await _cloudStorage.UploadFileAsync(icon, fileNameForStorage);
            string imageUrl = _configuration.GetSection("CloudStorageUrl").Value + fileNameForStorage;
            return imageUrl;
        }
    }
}