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
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public bool Active { get; set; } = false;
    }
}
