using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerUtils
{
    public static class Common
    {
        public static BlobClient CreateBlobClient(string connectionString, string container, string fileName)
        {
            BlobServiceClient blobServiceClient;

            blobServiceClient = new BlobServiceClient(connectionString);
            var blobContainreClient = BlobContainerClient(blobServiceClient, container);
            var blobClient = BlobClient(blobContainreClient, fileName);

            return blobClient;
        }

        public static BlobContainerClient BlobContainerClient(BlobServiceClient blobClient, string container)
        {
            return blobClient.GetBlobContainerClient(container);
        }

        public static BlobClient BlobClient(BlobContainerClient blobContainerClient, string fileName)
        {
            return blobContainerClient.GetBlobClient(fileName);
        }
    }
}
