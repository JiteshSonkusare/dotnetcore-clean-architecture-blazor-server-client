namespace Client.Infrastructure.Authentication.Interfaces
{
    /// <summary>
    /// When implemented, represents an authorization token, which can be auto-refreshed.
    /// </summary>
    public interface IAuthToken
    {
        /// <summary>
        /// Type of authorization token. e.g. Basic, Bearer or null.
        /// </summary>
        string Scheme { get; set; }

        /// <summary>
        /// Authorization token value.
        /// </summary>
        string Value { get; set; }
    }
}
