using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNet.Identity;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork db;

        private readonly IMapper mapper;

        public UserService(IUnitOfWork uow)
        {
            db = uow;
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<ApplicationUser, ApplicationUserDTO>(); });
            mapper = config.CreateMapper();
        }

        public OperationDetails CreateUser(ApplicationUserDTO user)
        {
            ApplicationUser appUser = db.UserManager.FindByName(user.UserName);
            if (appUser == null)
            {
                appUser = new ApplicationUser { Email = user.Email, UserName = user.UserName };
                db.UserManager.Create(appUser, user.Password);
                db.Commit();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "");
        }

        public OperationDetails DeleteUser(ApplicationUserDTO user)
        {
            ApplicationUser appUser = db.UserManager.FindByName(user.UserName);
            if (appUser != null)
            {
                db.UserManager.Delete(appUser);
                db.Commit();
                return new OperationDetails(true, "Удаление прошло успешно", "");
            }
            return new OperationDetails(false, "Пользователь, который должен быть удален не существует", "");
        }

        public IEnumerable<ApplicationUserDTO> GetAllUsers()
        {
            return mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserDTO>>(db.UserManager.Users);
        }

        public ApplicationUserDTO GetUserById(string id)
        {
            
            return mapper.Map<ApplicationUser, ApplicationUserDTO>(db.UserManager.FindById(id));
        }

        public ClaimsIdentity Authenticate(ApplicationUserDTO userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = db.UserManager.Find(userDto.UserName, userDto.Password);
            if (user != null)
                claim = db.UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
