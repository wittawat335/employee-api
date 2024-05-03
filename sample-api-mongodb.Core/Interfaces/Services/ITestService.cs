using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sample_api_mongodb.Core.Interfaces.Services
{
    public interface ITestService
    {
        string GenereateGreetText();
        List<string> GetCities();
    }
}
