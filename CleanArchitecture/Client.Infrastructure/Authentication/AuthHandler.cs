using Microsoft.Identity.Web;
using Client.Infrastructure.Exceptions;
using Client.Infrastructure.Configuration;
using Client.Infrastructure.Authentication.Helper;
using Client.Infrastructure.Authentication.Interfaces;

namespace Client.Infrastructure.Authentication
{
    /// <summary>
    /// If you have another auth provider, inherit ApiClientBase and implement. 
    /// </summary>
    public sealed class AuthHandler : IAuthHandler
    {
        private readonly AuthConfig _config;
        private readonly ITokenAcquisition _tokenAcquisition;

        public AuthHandler(AuthConfig config, ITokenAcquisition tokenAcquisition)
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