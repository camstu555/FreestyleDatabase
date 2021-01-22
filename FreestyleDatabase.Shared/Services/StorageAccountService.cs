﻿using Azure.Storage.Blobs;
using System.IO;
using System.Threading.Tasks;

namespace FreestyleDatabase.Shared.Services
{
    public class StorageAccountService
    {
        private const string AccountName = "storageaccountfrees9d77";
        private const string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=storageaccountfrees9d77;AccountKey=XpgB1oxqxEYqAcXEUCnKBHfopg4zAq3ckMPNXggt2uOuaqUGA4NlSbB63j6m/mTirXGSCcXGaqU8z1IosaDCbA==;EndpointSuffix=core.windows.net";
        private readonly BlobContainerClient _container;

        public StorageAccountService()
        {
            this._container = new BlobContainerClient(ConnectionString, AccountName);
        }

        public async Task SaveFile(string fileName, Stream fileStream)
        {
            var client = _container.GetBlobClient(fileName);

            await client.UploadAsync(fileStream);

            fileStream.Seek(0, SeekOrigin.Begin);
        }

        public async Task<(byte[], string)> GetFile(string fileName)
        {
            var client = _container.GetBlobClient(fileName);

            var ms = new MemoryStream();

            await client.DownloadToAsync(ms);

            ms.Seek(0, SeekOrigin.Begin);

            var bytes = ms.ToArray();

            return (bytes, Path.GetExtension(fileName));
        }

        public async Task<bool> HasFile(string fileName)
        {
            var client = _container.GetBlobClient(fileName);
            var result = await client.ExistsAsync();

            return result.Value;
        }

        public async Task DeleteFile(string fileName)
        {
            var client = _container.GetBlobClient(fileName);

            await client.DeleteIfExistsAsync();
        }
    }
}