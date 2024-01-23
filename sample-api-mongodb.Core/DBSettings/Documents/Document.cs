using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace sample_api_mongodb.Core.DBSettings.Documents
{
    public abstract class Document : IDocument
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        public DateTime CreatedAt => Id.CreationTime;
    }
}
