using ETicaretAPI.Client.Models.Authentication.Login;
using ETicaretAPI.Client.Models.Authentication.Register;
using ETicaretAPI.Client.Models.Authentication.TwoFactorAuthentication;
using ETicaretAPI.Client.Services.Authentication;
using ETicaretAPI.Client.Statics.Urls;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.Client.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IHttpContextAccessor httpContextAccessor;
        public AuthenticationController(IAuthenticationService authenticationService, IHttpContextAccessor httpContextAccessor)
        {
            this.authenticationService = authenticationService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            LoginCommandRequest request = new() { LoginDto = loginDto };
            var result = await authenticationService.LoginAsync(request, Statics.Urls.ApiUrls.Login);
            if (result.isSuccess == true && result.TFA == false)
                return RedirectToAction("Index", "Home");
            else if (result.isSuccess == true && result.TFA == true)
            {
                var routeValues = new RouteValueDictionary {
                    { "email", result.token}
                };
                return RedirectToAction("TwoFactorAuthentication", "Authentication", routeValues);
            }
            return View();
        }

        [HttpGet]                              
        [Route("TwoFactorAuthentication")]
        public async Task<IActionResult> TwoFactorAuthentication(string email)
        {
            return View("TwoFactorAuthentication", new TwoFactorAuthenticationViewModel { Email = email });
        }

        [HttpPost]
        [Route("TwoFactorAuthentication")]
        public async Task<IActionResult> TwoFactorAuthentication(string email, string code)
        {
            TwoFactorAuthenticationRequest request = new() { Email = email, Code = code };
            var result = await authenticationService.TwoFactorAuthenticationAsync(request,Statics.Urls.ApiUrls.TwoFactorAuthentication);
            if (result)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            RegisterCommandRequest request = new() { RegisterDto = registerDto };
            bool result = await authenticationService.RegisterAsync(request, ApiUrls.Register);
            if (result)
                return RedirectToAction("Index", "Home");
            return View();
        }

    }
}
