using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace BookingRevamp.Services
{
    public class GoogleStorageService
    {
        private readonly StorageClient _client;
        private readonly string _bucket;

        public GoogleStorageService(IConfiguration config)
        {
            var json = config["GOOGLE_APPLICATION_CREDENTIALS_JSON"]
                ?? throw new Exception("GOOGLE_APPLICATION_CREDENTIALS_JSON is missing");

            var credential = GoogleCredential.FromJson(json);

            _client = StorageClient.Create(credential);

            _bucket = config["GoogleCloud:BucketName"]
                ?? throw new Exception("GoogleCloud:BucketName is missing");
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var fileName =
                Guid.NewGuid() +
                Path.GetExtension(file.FileName);

            using var stream = file.OpenReadStream();

            await _client.UploadObjectAsync(
                _bucket,
                fileName,
                file.ContentType ?? "application/octet-stream",
                stream
            );

            return $"https://storage.googleapis.com/{_bucket}/{fileName}";
        }
    }
}