using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sample_api_mongodb.Core.DBSettings.Documents;
using sample_api_mongodb.Core.DBSettings;

namespace sample_api_mongodb.Core.Entities
{
    [BsonIgnoreExtraElements]
    public class Roles 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}
