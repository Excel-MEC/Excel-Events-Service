namespace API.Services.Interfaces
{
    public interface IEnvironmentService
    {
        public string PostgresDb { get; }
        public string GoogleCloudStorageBucket { get; }
        public string CloudStorageUrl{ get; }
        public string ApiPrefix { get; }
        public string SecretKey { get; }
        public string AccountsHost { get; }
        public string ServiceKey { get; }
        public string GoogleCredential { get; }
    }
}