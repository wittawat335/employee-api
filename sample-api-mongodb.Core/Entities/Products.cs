using MongoDB.Bson.Serialization.Attributes;
using sample_api_mongodb.Core.DBSettings;
using sample_api_mongodb.Core.DBSettings.Documents;

namespace sample_api_mongodb.Core.Entities
{
    [BsonCollection("products")]
    public class Products :  Document
    {
        [BsonElement("id")]
        public int ProductId { get; set; }
        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;
        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;
        [BsonElement("price")]
        public int Price { get; set; }
        [BsonElement("discountPercentage")]
        public double DiscountPercentage { get; set; }
        [BsonElement("rating")]
        public double Rating { get; set; }
        [BsonElement("stock")]
        public int Stock { get; set; }
        [BsonElement("brand")]
        public string Brand { get; set; } = string.Empty;
        [BsonElement("category")]
        public string Category { get; set; } = string.Empty;
        [BsonElement("thumbnail")]
        public string Thumbnail { get; set; } = string.Empty;
        [BsonElement("images")]
        public List<string>? images { get; set; }
        [BsonElement("active")]
        public bool Active { get; set; } 
    }
}
