using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace QROrganizer.Web.Controllers
{
    public class LoginCredentials
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        private async Task<bool> ValidateLogin(LoginCredentials creds)
        {
            // TODO: Share IdentityUser here
            var res = await _signInManager.PasswordSignInAsync(creds.Email, creds.Password, false, false);
            return res.Succeeded;
        }

        private ClaimsPrincipal GetPrincipal(LoginCredentials creds)
        {
            // TODO: Convert this to Claims Factory
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, creds.Email),
                new Claim(ClaimTypes.Email, creds.Email)
            };

            return new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials creds)
        {
            if (!await ValidateLogin(creds))
            {
                return Json(new { error = "login failed" });
            }

            var principal = GetPrincipal(creds);
            var user = await _userManager.FindByNameAsync(creds.Email.ToUpperInvariant());
            var roles = await _userManager.GetRolesAsync(user);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Json(new
            {
                name = principal.Identity.Name,
                email = principal.FindFirstValue(ClaimTypes.Email)
            });
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] LoginCredentials creds)
        {
            var user = new IdentityUser(creds.Email);
            var res = await _userManager.CreateAsync(user, creds.Password);

            if (!res.Succeeded)
            {
                return Json(new { error = "Create failed" });
            }

            return Json(new
            {
                email = user.Email
            });
        }
    }
}
