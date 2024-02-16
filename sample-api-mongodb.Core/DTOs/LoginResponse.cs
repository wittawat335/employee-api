namespace sample_api_mongodb.Core.DTOs
{
    public class LoginResponse
    {
        public string userId { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string fullname { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public List<string>? roles { get; set; }
        public string token { get; set; } = string.Empty;
        public string refreshToken { get; set; } = string.Empty;
        public string message { get; set; } = "Not Found User";
        public bool success { get; set; } = false;
    }
}
