namespace sample_api_mongodb.Core.DTOs
{
    public class LoginResponse
    {
        public bool Success { get;set; }
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public IList<string>? Roles { get; set; }

    }
}
