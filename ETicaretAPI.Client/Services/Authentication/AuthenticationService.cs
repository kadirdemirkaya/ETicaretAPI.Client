using ETicaretAPI.Client.Models.Authentication.Login;
using ETicaretAPI.Client.Models.Authentication.Register;
using ETicaretAPI.Client.Models.Authentication.TwoFactorAuthentication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ETicaretAPI.Client.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthenticationService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<(string? token, bool isSuccess, bool? TFA)> LoginAsync(LoginCommandRequest loginCommandRequest, string appUrl)
        {
            var json = JsonConvert.SerializeObject(loginCommandRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(appUrl, content);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            //var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseContent);
            var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginCommandResponse>(responseContent);

            string token = responseObject.token;
            httpContextAccessor.HttpContext.Session.SetString("JWToken", token);
            httpContextAccessor.HttpContext.Session.SetString("Email", loginCommandRequest.LoginDto.Email);

            return (token, responseObject.isSuccess, responseObject.TFA);
        }

        public async Task<bool> RegisterAsync(RegisterCommandRequest registerCommandRequest, string appUrl)
        {
            var json = JsonConvert.SerializeObject(registerCommandRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(appUrl, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<RegisterCommandResponse>(responseContent);
            response.EnsureSuccessStatusCode();
            return responseObject.response;
        }

        public async Task<bool> TwoFactorAuthenticationAsync(TwoFactorAuthenticationRequest twoFactorAuthentication, string appurl)
        {
            var json = JsonConvert.SerializeObject(twoFactorAuthentication);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(appurl, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<TwoFactorAuthenticationResponse>(responseContent);
            response.EnsureSuccessStatusCode();

            httpContextAccessor.HttpContext.Session.SetString("JWToken", responseObject.token);

            return responseObject.isSuccess;
        }
    }
}
