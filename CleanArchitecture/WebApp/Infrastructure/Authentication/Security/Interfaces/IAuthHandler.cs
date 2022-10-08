namespace WebApp.Infrastructure.Authentication.Security.Interfaces
{
	/// <summary>
	/// When implemented, helps to retrieve the Authorization token header.
	/// </summary>
	public interface IAuthHandler
	{
		Task<IAuthToken> GetAuthTokenAsync(CancellationToken cancellation);
	}
}