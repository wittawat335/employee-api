using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample_api_mongodb.Core.DTOs
{
    public class CreateRoleRequest
    {
        public string RoleName { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}
