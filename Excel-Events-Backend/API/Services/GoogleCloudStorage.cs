using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using API.Services.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;

namespace API.Services
{
    public class GoogleCloudStorage : ICloudStorage
    {
        private readonly GoogleCredential googleCredential;
        private readonly StorageClient storageClient;
        private readonly string bucketName;
        private readonly IEnvironmentService _env;

        public GoogleCloudStorage(IEnvironmentService env)
        {
            _env = env;
            googleCredential = GoogleCredential.FromJson(_env.GoogleCredential);
            storageClient = StorageClient.Create(googleCredential);
            bucketName = _env.GoogleCloudStorageBucket;
        }

        public async Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage)
        {
            using (var memoryStream = new MemoryStream())
            {
                await imageFile.CopyToAsync(memoryStream);
                var dataObject = await storageClient.UploadObjectAsync(bucketName, fileNameForStorage, null, memoryStream);
                dataObject.Acl ??= new List<ObjectAccessControl>();
                await storageClient.UpdateObjectAsync(dataObject, new UpdateObjectOptions
                {
                    PredefinedAcl = PredefinedObjectAcl.PublicRead
                });
                return dataObject.MediaLink;
            }
        }

        public async Task DeleteFileAsync(string fileNameForStorage)
        {
            await storageClient.DeleteObjectAsync(bucketName, fileNameForStorage);
        }
    }
}