using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Internship_2022.Application.Interfaces;
using Internship_2022.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_2022.Application.Services
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly string connectionString;
        private readonly string containterString;
        private readonly string SASKey;

        public AzureStorageService(IConfiguration configuration)
        {
            connectionString = configuration.GetSection("AzureStorageConnectionString").Value;
            containterString = configuration.GetSection("AzureStorageContainerString").Value;
            SASKey = configuration.GetSection("AzureStorageSASKey").Value;
        }

        public async Task<string> UploadStreamListing(string[] images)
        {
            var blobContainerClient = new BlobContainerClient(connectionString, "images");
            List<string> imagesLink = new List<string>();

            foreach (var item in images)
            {
                var splitted=item.Split(",");
                var first = splitted[0].Split(":");
                var last = first[1].Split(";");
                var extension = last[0].Split("/");
                var randomImageName = "image" + Guid.NewGuid().ToString();
                var blob = blobContainerClient.GetBlobClient(randomImageName + extension[1]);
                var bytes = Convert.FromBase64String(splitted[1]);
                var blobHttpHeader = new BlobHttpHeaders();
                blobHttpHeader.ContentType = last[0];
                Stream stream = new MemoryStream(bytes);
                
                var uploadBlob = await blob.UploadAsync(stream, blobHttpHeader);
                imagesLink.Add(containterString + "/" + randomImageName + extension[1]);

            }

            return string.Join(",", imagesLink.ToArray());
        }

        public async Task<string> UploadStreamUser(string image)
        {
            var blobContainerClient = new BlobContainerClient(connectionString, "images");

            var splitted = image.Split(",");
            var first = splitted[0].Split(":");
            var last = first[1].Split(";");
            var extension = last[0].Split("/");
            var randomImageName = "image" + Guid.NewGuid().ToString();
            var blob = blobContainerClient.GetBlobClient(randomImageName + extension[1]);
            var bytes = Convert.FromBase64String(splitted[1]);
            var blobHttpHeader = new BlobHttpHeaders();
            blobHttpHeader.ContentType = last[0];
            Stream stream = new MemoryStream(bytes);

            var uploadBlob = await blob.UploadAsync(stream, blobHttpHeader);

            return containterString + "/" + randomImageName + extension[1];
        }
    }
}
