using sample_api_mongodb.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample_api_mongodb.Core.Services
{
    public class TestService : ITestService
    {
        public string GenereateGreetText()
        {
            var dateTimeNow = DateTime.Now;
            return dateTimeNow.Hour switch
            {
                >= 5 and < 12 => "Morning",
                >= 12 and < 18 => "Afternoon",
                _ => "Evering"
            };
        }
    }
}
