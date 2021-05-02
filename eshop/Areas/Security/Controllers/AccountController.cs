using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eshop.Controllers;
using eshop.Models;
using eshop.Models.AplicationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eshop.Areas.Security.Controllers
{
    [Area("Security")]
    [AllowAnonymous]
    public class AccountController : Controller
    {      
        ISecurityApplicationService iSecure;
        private readonly ILogger<AccountController> logger;

        public AccountController(ISecurityApplicationService iSecure, ILogger<AccountController> logger)
        {
            this.iSecure = iSecure;
            this.logger = logger;
        }
        
        public IActionResult Login()
        {
            logger.LogInformation("Account login");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            logger.LogInformation("Account login");
            vm.LoginFailed = false;
            if (ModelState.IsValid)
            {
                bool isLogged = await iSecure.Login(vm);
                if (isLogged)
                {
                    return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", String.Empty), new { area = "" });
                }
                else
                {
                    vm.LoginFailed = true;                    
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            logger.LogInformation("Account logout");
            iSecure.Logout();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Register()
        {
            logger.LogInformation("Account register");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            logger.LogInformation("Account register");
            vm.ErrorDuringRegister = null;
            if (ModelState.IsValid)
            {
                vm.ErrorDuringRegister = await iSecure.Register(vm, Models.Identity.Roles.Customer);
                if (vm.ErrorDuringRegister == null)
                {
                    var lvm = new LoginViewModel()
                    {
                        UserName = vm.UserName,
                        Password = vm.Password,
                        RememberMe = true,
                        LoginFailed = false
                    };

                    return await Login(lvm);
                }
            }
            return View(vm);
        }
    }
}
