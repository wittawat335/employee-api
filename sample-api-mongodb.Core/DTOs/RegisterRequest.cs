using System.ComponentModel.DataAnnotations;

namespace sample_api_mongodb.Core.DTOs
{
    public class RegisterRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        //[Required]
        //public string Fullname { get; set; } = string.Empty;
        [Required]
        public List<string>? Roles { get; set; } 
        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public string Active { get; set; } = string.Empty;
    }
}
