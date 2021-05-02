using eshop.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eshop.Models.AplicationServices
{
    public interface ISecurityApplicationService
    {
        Task<string[]> Register(RegisterViewModel vm, Roles role);

        Task<bool> Login(LoginViewModel vm);

        Task Logout();

        Task<User> FindUserByName(string username);

        Task<User> FindUserByEmail(string email);

        Task<IList<string>> GetUserRoles(User user);

        Task<User> GetCurrentUser(ClaimsPrincipal claims);
    }
}
