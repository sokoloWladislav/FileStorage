using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using BLL.Infrastructure;

namespace BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        OperationDetails CreateUser(ApplicationUserDTO user);
        OperationDetails DeleteUser(ApplicationUserDTO user);
        ApplicationUserDTO GetUserById(string id);
        IEnumerable<ApplicationUserDTO> GetAllUsers();
        ClaimsIdentity Authenticate(ApplicationUserDTO user);
    }
}
