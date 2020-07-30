using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Async_Inn.Models;
using Async_Inn.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = configuration;
        }

        //api/account/register
        [Authorize(Policy = "DistrictAndPropertyManagers")]
        [HttpPost, Route("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName
            };
            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (result.Succeeded)
            {
                if (user.Email == _config["DistrictManagerSeed"])
                {
                    await _userManager.AddToRoleAsync(user, ApplicationRoles.DistrictManager);
                }
                await _signInManager.SignInAsync(user, false);
                return Ok();
            }
            return BadRequest("Invalid Registration");
        }

        [Authorize(Roles = ApplicationRoles.DistrictManager)]
        [HttpPost, Route("assign/role")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleDTO assignDTO)
        {
            var user = await _userManager.FindByEmailAsync(assignDTO.Email);
            await _userManager.AddToRoleAsync(user, assignDTO.Role);
            return Ok($"{assignDTO.Email} assigned to the role of {assignDTO.Role}");
        }

        [Authorize(Policy = "DistrictAndPropertyManagers")]
        [HttpPost, Route("assign/agent/role")]
        public async Task<IActionResult> AssignRoleToAgentUser(AssignRoleDTO assignDTO)
        {
            if (assignDTO.Role == "Agent")
            {
                var user = await _userManager.FindByEmailAsync(assignDTO.Email);
                await _userManager.AddToRoleAsync(user, ApplicationRoles.Agent);
                return Ok($"{assignDTO.Email} assigned to the role of {ApplicationRoles.Agent}");
            }
            else
            {
                return BadRequest("You are not authorized to assign roles other than Agent.");
            }
        }

        //api/account/login
        [HttpPost, Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(loginDTO.Email);

                //var identityRole = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
                var token = CreateToken(user);

                return Ok(new
                {
                    jwt = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return BadRequest("Invalid attempt");
        }

        private JwtSecurityToken CreateToken(ApplicationUser user)
        {
            var authClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("UserId", user.Id),
            };
            var token = AuthenticateToken(authClaims);
            return token;
        }

        private JwtSecurityToken AuthenticateToken(Claim[] cliams)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTKey"]));
            var token = new JwtSecurityToken
            (
                issuer: _config["JWTIssuer"],
                audience: _config["JWTIssuer"],
                expires: DateTime.UtcNow.AddHours(24),
                claims: cliams,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            return token;
        }
    }
}