namespace Client.Infrastructure.Configuration
{
    /// <summary>
    /// Inherrit this, when you want implement configuration for apis.
    /// </summary>
    public interface IClientConfig
    {
        /// <summary>
        /// Base URL for REST API to connect.
        /// </summary>
        string BaseUrl { get; }

        /// <summary>
        /// Timeout for API request. Default value will be 25 seconds.
        /// </summary>
        TimeSpan? Timeout { get; set; }
    }
}
