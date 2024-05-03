using sample_api_mongodb.Core.Interfaces.Services;

namespace sample_api_mongodb.Core.Services
{
    public class TestService : ITestService
    {
        private readonly TimeProvider _timeProvider;

        public TestService(TimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public string GenereateGreetText()
        {
            var start = _timeProvider.GetTimestamp();
            var end = _timeProvider.GetTimestamp();
            var dateTimeNow = _timeProvider.GetLocalNow();
            var diff = _timeProvider.GetElapsedTime(start, end);
            return dateTimeNow.Hour switch
            {
                >= 5 and < 12 => "Morning",
                >= 12 and < 18 => "Afternoon",
                _ => "Evering"
            };
        }

        public List<string> GetCities()
        {
            var cities = new List<string>
            {
                "Bangkok",
                "London",
                "New York"
            };

            return cities;
        }
    }
}
