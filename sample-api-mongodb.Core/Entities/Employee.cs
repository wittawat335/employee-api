using MongoDB.Bson.Serialization.Attributes;
using sample_api_mongodb.Core.DBSettings;
using sample_api_mongodb.Core.DBSettings.Documents;

namespace sample_api_mongodb.Core.Entities
{
    [BsonCollection("employees")]
    public class Employee : Document
    {
        [BsonElement("EmployeeId")]
        public string? EmployeeId { get; set; }

        [BsonElement("FirstName")]
        public string FirstName { get; set; } = string.Empty;

        [BsonElement("LastName")]
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";

        [BsonElement("PhoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;

        [BsonElement("Email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("Gender")]
        public string Gender { get; set; } = string.Empty;

        [BsonElement("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [BsonElement("Address")]
        public string Address { get; set; } = string.Empty;

        [BsonElement("DepartmentId")]
        public string DepartmentId { get; set; } = string.Empty;

        [BsonElement("Active")]
        public bool Active { get; set; } = false;
    }
}
