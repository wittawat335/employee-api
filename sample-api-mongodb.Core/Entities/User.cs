using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using sample_api_mongodb.Core.DBSettings.Documents;

namespace sample_api_mongodb.Core.Entities
{
    [BsonIgnoreExtraElements]
    public class User :  Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Username { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
