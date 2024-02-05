
namespace sample_api_mongodb.Core.DTOs
{
    public class UserDTO
    {
        public string id { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string fullname { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string roles { get; set; } = string.Empty;
        //public List<string>? roles { get; set; } 
        public string active { get; set; } = string.Empty;
    }
}
