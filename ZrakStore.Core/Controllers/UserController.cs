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
        private readonly IUserService userService;
        private readonly IPasswordHasher passwordHasher;

        public UserController(IUserService userService, IPasswordHasher passwordHasher)
        {
            this.userService = userService;
            this.passwordHasher = passwordHasher;
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> All()
        {
            var users = await userService.GetAllUsersAsync();
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
            if (await userService.GetUserByUsernameAsync(model.Username) != null)
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

            await userService.AddUserAsync(newUser);
            return View("Summary", $"API User '{model.Username}' successfully created.");
        }
    }
}