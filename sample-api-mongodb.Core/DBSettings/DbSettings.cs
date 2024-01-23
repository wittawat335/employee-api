
namespace sample_api_mongodb.Core.DBSettings
{
    public class DbSettings : IDbSettings
    {
        public string DatabaseName { get; set; }  = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
    }
}
