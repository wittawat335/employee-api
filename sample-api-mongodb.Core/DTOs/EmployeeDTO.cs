
namespace sample_api_mongodb.Core.DTOs
{
    public class EmployeeDTO
    {
        public string? Id { get; set; }
        public int EmployeeId { get; set;}
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public bool Active { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedOn { get; set; }
    }
}
