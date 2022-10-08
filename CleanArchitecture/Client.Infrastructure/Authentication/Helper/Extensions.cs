using System.Net.Http.Headers;
using Client.Infrastructure.Authentication.Interfaces;

namespace Client.Infrastructure.Authentication.Helper
{
    internal static class Extensions
    {
        public static async Task<AuthenticationHeaderValue> GetAuthorizationHeaderAsync(this IAuthToken token)
            => await Task.Run(() => new AuthenticationHeaderValue(token.Scheme, token.Value));
    }
}