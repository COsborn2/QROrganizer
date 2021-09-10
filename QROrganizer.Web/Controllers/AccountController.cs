using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QROrganizer.Data.Models;

namespace QROrganizer.Web.Controllers
{
    public class LoginCredentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials creds)
        {
            var user = await _userManager.FindByEmailAsync(creds.Email);

            var res = await _signInManager.PasswordSignInAsync(
                user,
                creds.Password,
                false,
                false);

            if (!res.Succeeded)
            {
                return new UnauthorizedResult();
            }

            return new OkResult();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] LoginCredentials creds)
        {
            var user = new ApplicationUser
            {
                UserName = creds.Email,
                Email = creds.Email
            };

            var res = await _userManager.CreateAsync(user, creds.Password);

            if (!res.Succeeded)
            {
                return new UnauthorizedResult();
            }

            return new OkResult();
        }
    }
}
