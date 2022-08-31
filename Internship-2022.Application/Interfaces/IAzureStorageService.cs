using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_2022.Application.Interfaces
{
    public interface IAzureStorageService
    {
        Task<string> UploadStreamListing(string[] images);
        Task<string> UploadStreamUser(string image);
    }
}
