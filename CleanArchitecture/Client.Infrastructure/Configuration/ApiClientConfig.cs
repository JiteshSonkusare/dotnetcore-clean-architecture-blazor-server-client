namespace Client.Infrastructure.Configuration
{
    public class ApiClientConfig : IClientConfig
    {
        public string BaseUrl { get; set; } = null!;

        public TimeSpan? Timeout { get; set; }
    }
}
