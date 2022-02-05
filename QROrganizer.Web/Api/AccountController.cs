using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QROrganizer.Data.Models;
using QROrganizer.Data.Services.Implementation;

namespace QROrganizer.Web.Api
{
    public class LoginCredentials
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ErrorResponse
    {
        public ICollection<string> Errors { get; set; }
    }

    [Route("api")]
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

            if (user is null)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<string>{"Account with that email could not be found"}
                });
            }

            var res = await _signInManager.PasswordSignInAsync(
                user,
                creds.Password,
                false,
                false);

            if (res is null || !res.Succeeded)
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    return BadRequest(new ErrorResponse
                    {
                        Errors = new List<string>{"Email is not verified - click link in email"}
                    });
                }

                return new UnauthorizedResult();
            }

            var roles = await _userManager.GetRolesAsync(user);

            var loggedInUser = new UserInfo
            {
                Email = user.Email,
                Roles = roles
            };

            return Ok(loggedInUser);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] LoginCredentials creds)
        {
            if (creds.Username.Length > 16)
            {
                return BadRequest();
            }

            if (creds.Password != creds.ConfirmPassword) return new UnauthorizedResult();

            var user = new ApplicationUser
            {
                Email = creds.Email,
                UserName = creds.Username
            };

            var res = await _userManager.CreateAsync(user, creds.Password);

            if (!res.Succeeded)
            {
                return new UnauthorizedObjectResult(res);
            }

            return new OkResult();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
