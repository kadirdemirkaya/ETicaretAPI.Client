namespace ETicaretAPI.Client.Models.Authentication.Login
{
    public class LoginCommandResponse
    {
        public string token { get; set; }
        public bool isSuccess { get; set; }

        public bool? TFA { get; set; }
    }
}
