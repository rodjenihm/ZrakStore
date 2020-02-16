using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ZrakStore.Core.Models;
using ZrakStore.Data.Entities;
using ZrakStore.Services;

namespace ZrakStore.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IPasswordHasher passwordHasher;

        public UserController(IUserService userService, IRoleService roleService, IPasswordHasher passwordHasher)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.passwordHasher = passwordHasher;
        }

        //[Authorize(Roles = "User, Admin")]
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
            await roleService.AddUserToRoleAsync(newUser, RoleType.User);
            return View("Summary", $"API User '{model.Username}' successfully created.");
        }
    }
}