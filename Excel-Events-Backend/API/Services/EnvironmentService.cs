using System;
using System.Text;
using API.Services.Interfaces;

namespace API.Services
{
    public class EnvironmentService: IEnvironmentService
    {
        public string PostgresDb { get; }
        public string GoogleCloudStorageBucket { get; }
        public string CloudStorageUrl{ get; }
        public string ApiPrefix { get; }
        public string SecretKey { get; }
        public string AccountsHost { get; }
        public string ServiceKey { get; }
        public string AccessToken { get; }
        public string Issuer { get; }
        public string GoogleCredential { get; }
        public EnvironmentService()
        {
             PostgresDb = Environment.GetEnvironmentVariable("POSTGRES_DB");
             GoogleCloudStorageBucket = Environment.GetEnvironmentVariable("GOOGLE_CLOUD_STORAGE_BUCKET");
             CloudStorageUrl = Environment.GetEnvironmentVariable("CLOUD_STORAGE_URL");
             ApiPrefix = Environment.GetEnvironmentVariable("API_PREFIX");
             SecretKey = Environment.GetEnvironmentVariable("SECRET_KEY");
             AccountsHost = Environment.GetEnvironmentVariable("ACCOUNTS_HOST");
             ServiceKey = Environment.GetEnvironmentVariable("SERVICE_KEY");
             AccessToken = Environment.GetEnvironmentVariable("ACCESS_TOKEN");
             Issuer = Environment.GetEnvironmentVariable("ISSUER");
             GoogleCredential = Encoding.UTF8.GetString(Convert.FromBase64String(Environment.GetEnvironmentVariable("GOOGLE_CREDENTIAL")!)) ;
        }
    }
}