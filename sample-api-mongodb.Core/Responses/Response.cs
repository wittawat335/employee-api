using sample_api_mongodb.Core.Commons;

namespace sample_api_mongodb.Core.Responses
{
    public class Response<T>
    {
        public bool success { get; set; } = false;
        public string message { get; set; } = Constants.StatusMessage.No_Data;
        public T? value { get; set; }
        public string? url { get; set; }
    }
}
