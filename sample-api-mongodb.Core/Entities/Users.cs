using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using sample_api_mongodb.Core.DBSettings;
using sample_api_mongodb.Core.DBSettings.Documents;

namespace sample_api_mongodb.Core.Entities
{
    //[BsonIgnoreExtraElements]
    //[BsonCollection("users")]
    //public class Users :  Document
    //{
    //    [BsonId]
    //    [BsonRepresentation(BsonType.ObjectId)]
    //    public string Id { get; set; } = string.Empty;
    //    public string Username { get; set; } = string.Empty;
    //    public string Fullname { get; set; } = string.Empty;
    //    public string Email { get; set; } = string.Empty;
    //}

    [BsonIgnoreExtraElements]
    public class Users 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
