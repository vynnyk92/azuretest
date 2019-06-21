using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AzureTest.Services
{
    public class ImageStore
    {
        CloudBlobClient cloudBlobClient;
        string baseUrl = "https://pshellovynny.blob.core.windows.net/";

        public ImageStore()
        {
            var storageCreds = new StorageCredentials("pshellovynny", "aA70GASDPsl8rwQBZOptHluBXgFxdDuZP7xvBDut90ajky3byPUUcgNDkRb5WJJZq7w3AAE9Y/HtKYLEV1XwKQ==");
            cloudBlobClient = new CloudBlobClient(new Uri(baseUrl), storageCreds);
        }

        internal async Task<string> SaveImage(Stream stream)
        {
            var id = Guid.NewGuid().ToString();
            var container = cloudBlobClient.GetContainerReference("images");
            var blob = container.GetBlockBlobReference(id);
            await blob.UploadFromStreamAsync(stream);

            return id;
        }

        internal string UriFor(string imageId)
        {

            var sasPolicy = new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessStartTime=DateTime.UtcNow.AddMinutes(-15),
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(15)
            };

            var container = cloudBlobClient.GetContainerReference("images");
            var blob = container.GetBlockBlobReference(imageId);
            var sas= blob.GetSharedAccessSignature(sasPolicy);


            return $"{baseUrl}images/{imageId}{sas}";
        }
    }
}
