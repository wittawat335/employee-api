namespace sample_api_mongodb.Core.DTOs
{
    public class LoginResponse
    {
        public bool success { get;set; }
        public string message { get; set; } = string.Empty;
        public string token { get; set; } = string.Empty;
        public string refreshToken { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string userId { get; set; } = string.Empty;
        public IList<string>? roles { get; set; }

    }
}
