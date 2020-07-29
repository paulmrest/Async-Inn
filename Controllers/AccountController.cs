using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Async_Inn.Models;
using Async_Inn.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Async_Inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //api/account/register
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
                await _signInManager.SignInAsync(user, false);
                return Ok();
            }
            return BadRequest("Invalid Registration");
        }

        //api/account/login
        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, false);
            if (result.Succeeded)
            {
                return Ok("Logged in");
            }
            return BadRequest("Invalid attempt");
        }
    }
}