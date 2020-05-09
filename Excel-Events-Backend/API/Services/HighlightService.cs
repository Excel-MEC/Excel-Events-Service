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
    public class HighlightService : IHighlightService
    {
        private readonly ICloudStorage _cloudStorage;
        private readonly IConfiguration _configuration;

        public HighlightService(ICloudStorage cloudStorage, IConfiguration configuration)
        {
            _cloudStorage = cloudStorage;
            _configuration = configuration;
        }

        public async Task<string> UploadHighlightImage(string name, IFormFile icon)
        {
            string fileNameForStorage = GetFilenameForStorage(name, icon.FileName);
            await _cloudStorage.UploadFileAsync(icon, fileNameForStorage);
            string imageUrl = _configuration.GetSection("CloudStorageUrl").Value + fileNameForStorage;
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