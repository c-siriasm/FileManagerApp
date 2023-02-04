using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace FileManagerUtils
{
    public static class BlobStorageHelper
    {
        public static async Task UploadFileToBlob(MemoryStream stream, string fileName)
        {
            var blobClient = Common.CreateBlobClient(ConfigurationManager.AppSettings["FileManagerStorageConnectionString"],
                ConfigurationManager.AppSettings["FileManagerContainer"], fileName);
            stream.Position = 0;
            await blobClient.UploadAsync(stream);
        }

        public static byte[] DownloadBlob(string fileName)
        {
            var blobClient = Common.CreateBlobClient(ConfigurationManager.AppSettings["FileManagerStorageConnectionString"],
               ConfigurationManager.AppSettings["FileManagerContainer"], fileName);

            if (blobClient.Exists())
            {
                using (var ms = new MemoryStream())
                {
                    blobClient.DownloadTo(ms);
                    return ms.ToArray();
                }
            }

            return null;
        }
    }
}
