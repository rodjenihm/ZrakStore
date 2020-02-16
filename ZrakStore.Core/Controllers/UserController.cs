using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ZrakStore.Data.Entities;
using ZrakStore.Services;
using ZrakStore.Core.Models;

namespace ZrakStore.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IWebService webService;
        private readonly IPasswordHasher passwordHasher;

        public UserController(IWebService webService, IPasswordHasher passwordHasher)
        {
            this.webService = webService;
            this.passwordHasher = passwordHasher;
        }

        //[Authorize(Roles = "User")]
        public async Task<IActionResult> All()
        {
            var users = await webService.GetAllUsersAsync();
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (await webService.GetUserByUsernameAsync(model.Username) != null)
            {
                return View("Summary", $"Username '{model.Username}' is already taken.");
            }

            User newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                Username = model.Username,
                PasswordHash = passwordHasher.HashPassword(model.Password)
            };

            await webService.AddUserAsync(newUser);
            await webService.AddUserToRoleAsync(newUser, RoleType.User);
            return View("Summary", $"API User '{model.Username}' successfully created.");
        }
    }
}