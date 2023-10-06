using ETicaretAPI.Client.Models.Authentication.Login;
using ETicaretAPI.Client.Models.Authentication.Register;
using ETicaretAPI.Client.Models.Authentication.TwoFactorAuthentication;

namespace ETicaretAPI.Client.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<(string? token,bool isSuccess,bool? TFA)> LoginAsync(LoginCommandRequest loginCommandRequest, string appUrl);
        Task<bool> RegisterAsync(RegisterCommandRequest registerCommandRequest, string appUrl);
        Task<bool> TwoFactorAuthenticationAsync(TwoFactorAuthenticationRequest twoFactorAuthentication, string appurl);
    }
}
