namespace ETicaretAPI.Client.Models.Authentication.TwoFactorAuthentication
{
    public class TwoFactorAuthenticationRequest
    {
        public string Code { get; set; }
        public string Email { get; set; }
    }
}
