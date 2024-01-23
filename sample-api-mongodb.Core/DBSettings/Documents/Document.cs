using MongoDB.Bson;
namespace sample_api_mongodb.Core.DBSettings.Documents
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }
        public DateTime CreatedAt => Id.CreationTime;
    }
}
