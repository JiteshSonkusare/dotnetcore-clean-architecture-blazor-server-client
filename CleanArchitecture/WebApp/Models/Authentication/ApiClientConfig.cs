namespace WebApp.Models.Authentication
{
    public class ApiClientConfig : IApiClientConfig
    {
        public string BaseUrl { get; set; } = null!;

        public string Scopes { get; set; } = null!;

        public TimeSpan? Timeout { get; set; }
    }
}
