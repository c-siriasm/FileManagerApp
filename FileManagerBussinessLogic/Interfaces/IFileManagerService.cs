using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagerBussinessLogic
{
    public interface IFileManagerService
    {
        Task<bool> NewUpload(FileManagerDTO.Models.File model, Stream fileStream);

        Task<List<FileManagerDTO.Models.File>> GetFiles();

        byte[] DownloadFile(string fileName);
    }
}
