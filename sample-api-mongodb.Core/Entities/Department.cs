using MongoDB.Bson.Serialization.Attributes;
using sample_api_mongodb.Core.DBSettings;
using sample_api_mongodb.Core.DBSettings.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample_api_mongodb.Core.Entities
{
    [BsonCollection("department")]
    public class Department : Document
    {
        public string? DepartmentId {  get; set; }
        public string? DepartmentName { get; set; }
        public bool Active { get; set; } = false;
    }
}
