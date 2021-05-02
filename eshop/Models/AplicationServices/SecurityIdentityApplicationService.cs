﻿using eshop.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eshop.Models.AplicationServices
{
    public class SecurityIdentityApplicationService : ISecurityApplicationService
    {
        UserManager<User> userManager;
        SignInManager<User> signInManager;

        public SecurityIdentityApplicationService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }


        public Task<User> FindUserByEmail(string email)
        {
            return userManager.FindByEmailAsync(email);
        }

        public Task<User> FindUserByName(string username)
        {
            return userManager.FindByNameAsync(username);
        }

        public Task<User> GetCurrentUser(ClaimsPrincipal principal)
        {
            return userManager.GetUserAsync(principal);
        }

        public Task<IList<string>> GetUserRoles(User user)
        {
            return userManager.GetRolesAsync(user);
        }

        public async Task<bool> Login(LoginViewModel vm)
        {
            var result = await signInManager.PasswordSignInAsync(vm.UserName,vm.Password, vm.RememberMe, true);

            return result.Succeeded;
        }

        public Task Logout()
        {
            return signInManager.SignOutAsync();
        }

        public async Task<string[]> Register(RegisterViewModel vm, Roles role)
        {
            User user = new User()
            {
                UserName = vm.UserName,
                Name = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                PhoneNumber = vm.PhoneNumber
            };

            string[] errors = null;
            var result = await userManager.CreateAsync(user, vm.Password);
            if (result.Succeeded)
            {
                var resultRole = await userManager.AddToRoleAsync(user, role.ToString());

                if(resultRole.Succeeded == false)
                {
                    for (int i = 0; i < resultRole.Errors.Count(); i++)
                        result.Errors.Append(resultRole.Errors.ElementAt(i));
                }
            }

            if(result.Errors != null && result.Errors.Count() > 0)
            {
                errors = new string[result.Errors.Count()];
                for(int i = 0; i < result.Errors.Count(); i++)
                {
                    errors[i] = result.Errors.ElementAt(i).Description;
                }
            }
            return errors;
        }
    }
}
