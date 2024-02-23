using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Data;
namespace sample_api_mongodb.Core.DBSettings.Documents
{
    public abstract class Document : IDocument
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; } 
        public DateTime ModifiedOn { get; set; } 
        public string ModifiedBy { get; set; } 
    }
}
