using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileManagerData.Interfaces;
using FileManagerUtils;

namespace FileManagerBussinessLogic
{
    public class FileManagerService : IFileManagerService
    {
        ICosmosRepository<FileManagerDTO.Models.File> _repo;

        public FileManagerService(ICosmosRepository<FileManagerDTO.Models.File> repo)
        {
            _repo = repo;
        }

        public async Task<List<FileManagerDTO.Models.File>> GetFiles()
        {
            var response = await _repo.GetItems();

            return response.ToList();
        }

        public async Task<bool> NewUpload(FileManagerDTO.Models.File model, Stream fileStream)
        {
            //get memorystream
            MemoryStream ms = new MemoryStream();
            fileStream.CopyTo(ms);
            
            //upload to blob
            await BlobStorageHelper.UploadFileToBlob(ms, model.Name);

            //upload to cosmos
            await _repo.CreateItem(model);

            return true;
        }

        public byte[] DownloadFile(string fileName) 
        {
            return BlobStorageHelper.DownloadBlob(fileName);
        }
    }
}
