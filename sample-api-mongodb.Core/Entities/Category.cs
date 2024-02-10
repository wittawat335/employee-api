using MongoDB.Bson.Serialization.Attributes;
using sample_api_mongodb.Core.DBSettings;
using sample_api_mongodb.Core.DBSettings.Documents;

namespace sample_api_mongodb.Core.Entities
{
    [BsonCollection("category")]
    public class Category : Document
    {
        [BsonElement("id")]
        public int CategoryId { get; set; }
        [BsonElement("name")]
        public string CategoryName { get; set; } = string.Empty;
        
        [BsonElement("active")]
        public bool Active { get; set; }

    }
}
