﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ZrakStore.Core.Config;
using ZrakStore.Core.Models;
using ZrakStore.Services;

namespace ZrakStore.Auth.TokenServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly IPasswordHasher passwordHasher;
        private readonly JwtConfig jwtConfig;

        public TokenController(IUserService userService, IRoleService roleService, IPasswordHasher passwordHasher, IOptions<JwtConfig> jwtConfig)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.passwordHasher = passwordHasher;
            this.jwtConfig = jwtConfig.Value;
        }

        [HttpPost]
        public async Task<ActionResult> Post(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await userService.GetUserByUsernameAsync(model.Username);

            if (user == null)
                return BadRequest(new { message = "Username is incorrect" });

            if (!passwordHasher.VerifyHashedPassword(model.Password, user.PasswordHash))
                return BadRequest(new { message = "Password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtConfig.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(),
                Expires = DateTime.UtcNow.AddSeconds(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtConfig.Issuer,
                Audience = jwtConfig.Audience
            };

            var userRoles = await roleService.GetUserRolesAsync(user);
            tokenDescriptor.Subject.AddClaim(new Claim("username", user.Username));
            foreach (var role in userRoles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(securityToken);

            return Ok(new { Jwt = jwt });
        }
    }
}