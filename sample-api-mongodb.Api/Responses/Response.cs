namespace sample_api_mongodb.Api.Responses
{
    public class Response<T>
    {
        public T payload { get; set; }
        public string message { get; set; } = string.Empty;
        public bool isSuccess { get; set; } = false;
    }
}
