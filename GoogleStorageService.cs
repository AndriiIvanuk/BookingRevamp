using Google.Cloud.Storage.V1;

namespace BookingRevamp.Services
{
    public class GoogleStorageService
    {
        private readonly StorageClient _client;
        private readonly string _bucket;

        public GoogleStorageService(IConfiguration config)
        {
            _client = StorageClient.Create();

            _bucket =
                config["GoogleCloud:BucketName"]
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

            return
                $"https://storage.googleapis.com/{_bucket}/{fileName}";
        }
    }
}