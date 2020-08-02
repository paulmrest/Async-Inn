using Async_Inn.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Async_Inn.Models.Services
{
    public class UserService
    {
        private AsyncInnDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AsyncInnDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
    }
}
