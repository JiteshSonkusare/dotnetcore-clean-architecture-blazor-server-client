using System.Net.Http.Headers;
using WebApp.Infrastructure.Authentication.Security.Interfaces;

namespace WebApp.Infrastructure.Authentication.Security
{
    internal static class Extensions
    {
        public static AuthenticationHeaderValue GetAuthorizationHeader(this IAuthToken token)
            => new AuthenticationHeaderValue(token.Scheme, token.Value);

        public static async Task<AuthenticationHeaderValue> GetAuthorizationHeaderasync(this IAuthToken token)
        {
            return await Task.Run(() => new AuthenticationHeaderValue(token.Scheme, token.Value));
        }
    }
}