using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample_api_mongodb.Core.DTOs
{
    public class RefreshTokenDTO
    {
        public string Token { get; set; } = string.Empty;
        public Guid UserId { get; set; } 
    }
}
