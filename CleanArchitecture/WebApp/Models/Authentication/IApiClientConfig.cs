namespace WebApp.Models.Authentication
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApiClientConfig
    {
        /// <summary>
        /// Base URL for REST API to connect.
        /// </summary>
        string BaseUrl { get; }

        /// <summary>
        /// Timeout for API request. Default value will be 25 seconds.
        /// </summary>
        TimeSpan? Timeout { get; set; }

        /// <summary>
        /// Scopes
        /// </summary>
		string Scopes { get; }
    }
}
