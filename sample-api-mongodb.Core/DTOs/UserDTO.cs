
namespace sample_api_mongodb.Core.DTOs
{
    public class UserDTO
    {
        public string id { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string fullname { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        //public IList<string>? roles { get; set; }
        public string roles { get; set; } = string.Empty;
        public IList<string>? logins { get; set; }
        public IList<string>? tokens { get; set; }
        public IList<string>? claims { get; set; }
        public bool active { get; set; }
    }
}
