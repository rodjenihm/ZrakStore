using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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

        public IActionResult Summary()
        {
            return View();
        }

        [Authorize]
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
                return View(nameof(Summary), $"Username '{model.Username}' is already taken.");
            }

            User newUser = new User
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                Username = model.Username,
                PasswordHash = passwordHasher.HashPassword(model.Password)
            };

            await userService.AddUserAsync(newUser);
            await roleService.AddUserToRoleAsync(newUser, RoleType.user);
            return View(nameof(Summary), $"API User '{model.Username}' successfully created.");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://localhost:44344/api/");
            //    var responseTask = await client.PostAsJsonAsync("token", model);
            //    var resultString = responseTask.Content.ReadAsStringAsync().Result;
            //    var result = JsonConvert.DeserializeObject<JObject>(resultString);

            //    if (responseTask.IsSuccessStatusCode)
            //    {
            //        var token = result.Value<string>("jwt");
            //        HttpContext.Response.Cookies.Append("token", token);
            //        return View("Summary", "Login successfull.");
            //    }
            //    else
            //    {
            //        var message = result.Value<string>("message");
            //        return View("Summary", message);
            //    }
            //}

            var user = await userService.GetUserByUsernameAsync(model.Username);

            if (user == null || !passwordHasher.VerifyHashedPassword(model.Password, user.PasswordHash))
            {
                return View(nameof(Summary), "The username or password is incorrect");
            }

            var zrakClaims = new List<Claim>();
            zrakClaims.Add(new Claim(ClaimTypes.Name, user.Username));
            var userRoles = await roleService.GetUserRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                zrakClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var zrakIdentity = new ClaimsIdentity(zrakClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(new[] { zrakIdentity });

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

            return string.IsNullOrEmpty(returnUrl) ? View(nameof(Summary), $"Login successfull.") : (IActionResult)LocalRedirect(returnUrl);
        }

        public async Task<IActionResult> Logout(string returnUrl)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return LocalRedirect(returnUrl);
        }
    }
}