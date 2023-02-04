using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FileManagerData;
using FileManagerData.Interfaces;
using FileManagerDTO.Models;
using FileManagerBussinessLogic;

namespace FileManagerApp.Controllers
{
    [Authorize]
    public class FileManagerController : Controller
    {
        IFileManagerService _fileManagerService;

        public FileManagerController(IFileManagerService fileManagerService)
        {
            _fileManagerService = fileManagerService;
        }

        // GET: FileManager
        public ActionResult UploadFile()
        {
            return View();
        }

        public async Task<ActionResult> List()
        {
            var files = await _fileManagerService.GetFiles();

            return View(files);
        }

        [HttpPost]
        public async Task<ActionResult> UploadFile(HttpPostedFileBase file, string Description)
        {
            if (file == null)
            {
                ViewBag.Message = "No file was selected.";

                return View();
            }

            if (string.IsNullOrEmpty(Description))
            {
                ViewBag.Message = "Please set a Description.";

                return View();
            }

            try
            {
                var request = new FileManagerDTO.Models.File()
                {
                    id = Guid.NewGuid().ToString(),
                    Name = file.FileName,
                    CreatedBy = User.Identity.Name,
                    CreatedDate = DateTime.Now,
                    Description = Description
                };

                await _fileManagerService.NewUpload(request, file.InputStream);

                ViewBag.Message = "Uploaded successfully.";
            }
            catch
            {
                ViewBag.Message = "An error occurred.";
            }


            return View();
        }

        public FileResult DownloadFile(string fileName)
        {
            var bytes = _fileManagerService.DownloadFile(fileName);

            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}