using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Data;
namespace sample_api_mongodb.Core.DBSettings.Documents
{
    public abstract class Document : IDocument
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; } = "System";
        public DateTime ModifiedOn { get; set; } = DateTime.Now;
        public string ModifiedBy { get; set; } = "System";
    }
}
