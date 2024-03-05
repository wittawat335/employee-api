using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample_api_mongodb.Core.DTOs
{
    public class DepartmentDTO
    {
        public string? id { get;set; }
        public string? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string Active { get; set; } = "1";
        public string? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
