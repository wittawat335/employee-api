using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using sample_api_mongodb.Core.DBSettings;
using sample_api_mongodb.Core.DBSettings.Documents;

namespace sample_api_mongodb.Core.Entities
{
    [BsonIgnoreExtraElements]
    public class Users 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        //public string? Roles { get; set; }
        public IList<string>? Roles { get; set; }
        public IList<string>? Logins { get; set; }
        public IList<string>? Tokens { get; set; }
        public IList<string>? Claims { get; set; }
        public bool Active { get; set; }
    }
}
