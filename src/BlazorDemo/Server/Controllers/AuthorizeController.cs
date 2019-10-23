using BlazorDemo.Shared;
using Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDemo.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly AppUserManager _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthorizeController(AppUserManager userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInputModel parameters)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(state => state.Errors)
                                                                        .Select(error => error.ErrorMessage)
                                                                        .FirstOrDefault());

            var user = await _userManager.FindUniqueAsync(parameters.Key);
            if (user == null) return BadRequest("User does not exist");
            var singInResult = await _signInManager.CheckPasswordSignInAsync(user, parameters.Password, false);
            if (!singInResult.Succeeded) return BadRequest("Invalid password");

            await _signInManager.SignInAsync(user, parameters.RememberMe);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> LoginViaSmsCode(LoginViaCodeInputModel parameters)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(state => state.Errors)
                                                                        .Select(error => error.ErrorMessage)
                                                                        .FirstOrDefault());

            var user = await _userManager.FindByPhoneNumberUniqueOrDefaultAsync(parameters.Key);
            if (user == null) return BadRequest("User does not exist");
            var singInResult = await _signInManager.CheckPasswordSignInAsync(user, parameters.Code, false);
            if (!singInResult.Succeeded) return BadRequest("Invalid password");

            await _signInManager.SignInAsync(user, parameters.RememberMe);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> LoginViaEmailCode(LoginViaCodeInputModel parameters)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(state => state.Errors)
                                                                        .Select(error => error.ErrorMessage)
                                                                        .FirstOrDefault());

            var user = await _userManager.FindByEmailAsync(parameters.Key);
            if (user == null) return BadRequest("User does not exist");
            var singInResult = await _signInManager.CheckPasswordSignInAsync(user, parameters.Code, false);
            if (!singInResult.Succeeded) return BadRequest("Invalid password");

            await _signInManager.SignInAsync(user, parameters.RememberMe);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterInputModel parameters)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(state => state.Errors)
                                                                        .Select(error => error.ErrorMessage)
                                                                        .FirstOrDefault());

            var user = new AppUser();
            user.UserName = parameters.UserName;
            var result = await _userManager.CreateAsync(user, parameters.Password);
            if (!result.Succeeded) return BadRequest(result.Errors.FirstOrDefault()?.Description);

            return await Login(new LoginInputModel
            {
                Key = parameters.UserName,
                Password = parameters.Password
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        public UserInfo UserInfo()
        {
            //var user = await _userManager.GetUserAsync(HttpContext.User);
            return BuildUserInfo();
        }

        private UserInfo BuildUserInfo()
        {
            return new UserInfo
            {
                IsAuthenticated = User.Identity.IsAuthenticated,
                UserName = User.Identity.Name,
                ExposedClaims = User.Claims
                    //Optionally: filter the claims you want to expose to the client
                    //.Where(c => c.Type == "test-claim")
                    .ToDictionary(c => c.Type, c => c.Value)
            };
        }
    }
}
