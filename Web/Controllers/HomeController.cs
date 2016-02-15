using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
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
            ViewBag.Layout = User.Identity.IsAuthenticated ? "~/Views/Shared/AuthLayout.cshtml" : "~/Views/Shared/NotAuthLayout.cshtml";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Layout = User.Identity.IsAuthenticated ? "~/Views/Shared/AuthLayout.cshtml" : "~/Views/Shared/NotAuthLayout.cshtml";

            return View();
        }

        public ActionResult Public()
        {
            ViewBag.Layout = User.Identity.IsAuthenticated ? "~/Views/Shared/AuthLayout.cshtml" : "~/Views/Shared/NotAuthLayout.cshtml";
            var fileConfig = new MapperConfiguration(cfg => { cfg.CreateMap<FileDTO, FileViewModel>(); });
            IMapper fileMapper = fileConfig.CreateMapper();
            List<FileViewModel> files = fileMapper.Map<IEnumerable<FileDTO>, List<FileViewModel>>(FileService.GetAllPublicFiles());
            return View(files);
        }

        public ActionResult Popular()
        {
            ViewBag.Layout = User.Identity.IsAuthenticated ? "~/Views/Shared/AuthLayout.cshtml" : "~/Views/Shared/NotAuthLayout.cshtml";
            var fileConfig = new MapperConfiguration(cfg => { cfg.CreateMap<FileDTO, FileViewModel>(); });
            IMapper fileMapper = fileConfig.CreateMapper();
            List<FileViewModel> files = fileMapper.Map<IEnumerable<FileDTO>, List<FileViewModel>>(FileService.GetAllPublicFiles());
            //Should be fixed!
            return View(files);
        }
    }
}