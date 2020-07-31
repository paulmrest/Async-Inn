using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
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
using Microsoft.Net.Http.Headers;
using SQLitePCL;

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
        //[AllowAnonymous]
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
                return await SetUpUserRole(registerDTO, user);
            }
            return BadRequest($"Invalid Registration, {result.ToString()}");
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
            //get the role of the currently logged in user
            var userRole = User.Claims.Where(x => x.Type == ClaimTypes.Role);
            if (userRole.FirstOrDefault().Value.ToLower() == ApplicationRoles.DistrictManager.ToLower() || assignDTO.Role.ToLower() == "agent")
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

                var identityRole = await _userManager.GetRolesAsync(user);
                var token = CreateToken(user, identityRole.ToList());

                return Ok(new
                {
                    jwt = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return BadRequest("Invalid attempt");
        }

        /// <summary>
        /// Private helper method. Sets up new user role from RegisterRTO for the ApplicationUser object.
        /// </summary>
        /// <param name="registerDTO">
        /// RegisterRTO: object containing data for the new user being registered
        /// </param>
        /// <returns>
        /// Task<IActionResult>: a message whether registering the new user succeeded
        /// </returns>
        private async Task<IActionResult> SetUpUserRole(RegisterDTO registerDTO, ApplicationUser user)
        {
            //get the role of the currently logged in user
            var userRole = User.Claims.Where(x => x.Type == ClaimTypes.Role);
            if (userRole.FirstOrDefault().Value.ToLower() == ApplicationRoles.DistrictManager.ToLower() || registerDTO.Role.ToLower() == ApplicationRoles.Agent.ToLower())
            {
                await _userManager.AddToRoleAsync(user, GetRole(registerDTO));
            }
            else
            {
                return BadRequest("You are not authorized to create a new user with that role.");
            }
            await _signInManager.SignInAsync(user, false);
            return Ok($"{registerDTO.Email} created and assigned a role of {registerDTO.Role}");
        }

        /// <summary>
        /// Private helper method. Matches user input to ApplicationRoles.
        /// </summary>
        /// <param name="registerDTO">
        /// RegisterRTO: object containing data for the new user being registered, including the role
        /// </param>
        /// <returns>
        /// string: the string representation of the role from ApplicationRoles
        /// </returns>
        private string GetRole(RegisterDTO registerDTO)
        {
            string role = "";
            switch (registerDTO.Role.ToLower())
            {
                case "district manager":
                    role = ApplicationRoles.DistrictManager;
                    break;
                case "property manager":
                    role = ApplicationRoles.PropertyManager;
                    break;
                case "agent":
                    role = ApplicationRoles.Agent;
                    break;
                default:
                    break;
            }
            return role;
        }

        /// <summary>
        /// Private helper method. Creates a new JwtSecurityToken from ApplicationUser object.
        /// </summary>
        /// <param name="user">
        /// 
        /// </param>
        /// <param name="roles">
        /// 
        /// </param>
        /// <returns>
        /// JwtSecurityToken: 
        /// </returns>
        private JwtSecurityToken CreateToken(ApplicationUser user, List<string> roles)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("UserId", user.Id)
            };
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var token = AuthenticateToken(authClaims);
            return token;
        }

        private JwtSecurityToken AuthenticateToken(List<Claim> claims)
        {
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTKey"]));
            var token = new JwtSecurityToken
            (
                issuer: _config["JWTIssuer"],
                audience: _config["JWTIssuer"],
                expires: DateTime.UtcNow.AddHours(24),
                claims: claims,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            return token;
        }
    }
}