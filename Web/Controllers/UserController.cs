using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Web.Models;

namespace Web.Controllers
{
     [Authorize]
    public class UserController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IFileService FileService
        {
            get
            {
                return HttpContext.GetOwinContext().Get<IFileService>();
            }
        }

        public ActionResult Index()
        {
            ViewBag.Name = User.Identity.Name;
            var fileConfig = new MapperConfiguration(cfg => { cfg.CreateMap<FileDTO, FileViewModel>(); });
            IMapper fileMapper = fileConfig.CreateMapper();
            List<FileViewModel> files = fileMapper.Map<IEnumerable<FileDTO>, List<FileViewModel>>(FileService.GetAllUserFiles(User.Identity.GetUserId()));
            return View(files);
        }

        public FileContentResult GetFile(int id)
        {
            Mapper.CreateMap<FileDTO, FileViewModel>();
            FileService.IncrementDownloadCount(id);
            FileViewModel file = Mapper.Map<FileDTO, FileViewModel>(FileService.GetFileById(id));
            return File(file.FileBytes, file.FileType, file.FileName);
        }

        public ActionResult CreateFile()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateFile(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                byte[] fileBytes = null;
                string fileName = null;
                using (var binaryReader = new BinaryReader(upload.InputStream))
                {
                    fileBytes = binaryReader.ReadBytes(upload.ContentLength);
                    fileName = upload.FileName;
                }
                FileDTO file = new FileDTO
                {
                    FileName = fileName,
                    FileBytes = fileBytes,
                    ApplicationUserId = User.Identity.GetUserId(),
                    IsPrivate = false
                };
                OperationDetails result = FileService.CreateFile(file);
                if (!result.Succedeed)
                    return RedirectToAction("CreateFile");
            }
            return RedirectToAction("Index", "User");
        }
	}
}