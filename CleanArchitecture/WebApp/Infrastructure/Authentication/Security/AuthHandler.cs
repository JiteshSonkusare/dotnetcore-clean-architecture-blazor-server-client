using WebApp.Exceptions;
using Microsoft.Identity.Web;
using WebApp.Models.Authentication;
using WebApp.Infrastructure.Authentication.Client;
using WebApp.Infrastructure.Authentication.Security.Interfaces;

namespace WebApp.Infrastructure.Authentication.Security
{
	public sealed class AuthHandler : ApiClientBase, IAuthHandler
	{
		private readonly ApiClientConfig _config;
		private readonly ITokenAcquisition _tokenAcquisition;
		
		public AuthHandler(ApiClientConfig config, ITokenAcquisition tokenAcquisition) : base(config: config, authHandler: null)
		{
			_config = config;
			_tokenAcquisition = tokenAcquisition;
		}

		/// <summary>
		/// Get jwt token from Azure Active Directory
		/// </summary>
		/// <param name="cancellation"></param>
		/// <returns>Access Token</returns>
		async Task<IAuthToken> IAuthHandler.GetAuthTokenAsync(CancellationToken cancellation)
		{
			try
			{
				var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(new string[] { _config.Scopes });
				return new TokenResponse
				{
					Access_Token = accessToken,
					Token_Type = "Bearer"
				};
			}
			catch (Exception ex)
			{
				throw new GeneralApplicationException("Error occurred while executing get token for user!", ex);
			}
		}
	}
}